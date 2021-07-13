using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using COVID.DB;

namespace COVID.SOM
{
    public static class EntrenarSom
    {
        static som self;
        static matriz mapa;
        static database db;
        static List<double[]> entrada;
        
        static string SomPath = @"C:\SW\EntrenamientoSOM.bin";

       // static string SomPath = @"C:\SW\EntrenamientoSOM.bin";

        public static void fit()
        {
            db = new database();
            entrada = db.dataSom();
            Random r = new Random();

            mapa = new matriz(19,10,10,r);// cantidada de pesos, ancho, alto
            self = new som(mapa);

            self.entrenar(entrada,0.1,500);  //REVISAR VALORES

            FileStream fs = new FileStream(SomPath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, self);
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

        }
        public static som clasificar(som mapa)
        {
            db = new database();
            entrada = db.dataSom();
            self = mapa;
            self.clasificar(entrada);
            return self;
        }
        public static som carga()
        {
            FileStream fs = new FileStream(SomPath, FileMode.Open); 
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                self = (som)formatter.Deserialize(fs);
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

            return self;
        }
    }
}
