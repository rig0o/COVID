using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using COVID.DB;
using COVID.MLP;
using COVID.Vista;

namespace COVID
{
    static class Program
    {
        static List<double[]> data;
        static List<double[]> target;
        static string MlpPath = @"D:\SW\Entrenada.bin";          ///antes se llamaba Entrenada
        static string SomPath = @"D:\SW\EntrenamientoSOM.bin";
        static Mlp red;

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            database db = new database();

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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mlp(db));


            //database db = new database();
            //data = db.datax();
            //target = db.datay();

            //foreach (var x in data[0])
            //{
            //    Console.Write(x);
            //}

        }
    }
}
