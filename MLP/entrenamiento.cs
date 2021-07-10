using COVID.DB;
using COVID.MLP.capas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace COVID.MLP
{
    class entrenamiento
    {
        static Mlp red;
        static List<Capa> capas;
        static List<double[]> entrada;
        static List<double[]> salida;
        static database db;
        static string MlpPath = @"C:\SW\EntrenamientoMLP.bin";

        static public void fit()
        {
            db = new database();
            entrada = db.datax();
            salida = db.datay();

         
            Random r = new Random();
            capas = new List<Capa>();
            capas.Add(new CapaRelu(14, 14, r));   //capa 
            capas.Add(new CapaRelu(14, 10, r));   //capa oculta Sigmoide 
            capas.Add(new CapaSig(10, 10, r));    //capa salida
            red = new Mlp(capas);
            //0.1, 0.075, 90000 -> malo
            //  0.1, 0.01, 20000 bueno
            while (!red.Entrenar(entrada, salida, 0.09, 0.01, 20000))  //entrada a la red - salida esperada - aprendizaje - error maximo permitido - iteraciones
            {
                Random rand = new Random();
                capas = new List<Capa>();
                capas.Add(new CapaRelu(14, 14, rand));   ///capa 
                capas.Add(new CapaRelu(14, 10, rand));   // capa oculta Sigmoide 
                capas.Add(new CapaSig(10, 10, rand));    //capa salida
                red = new Mlp(capas);
            }

            FileStream fs = new FileStream(MlpPath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, red);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("No se pudo serializar, motivo: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            double[] prueba = db.NormInverse(red.Forward_propagation(entrada[200]));

            foreach (var p in prueba)
                Console.Write(p + "|-|");

            Console.WriteLine("\n ---Salida esperada--");

            foreach (var p in db.NormInverse(salida[200]))
                Console.Write(p + "|-|");

        }
        static public Mlp carga()
        {
            FileStream fs = new FileStream(MlpPath, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                red = (Mlp)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Error en deserializacion, motivo :" + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            return red;
        }
    }
}
