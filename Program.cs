using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using COVID.DB;
using COVID.Vista;

namespace COVID
{
    static class Program
    {
        static List<double[]> data;
        static List<double[]> target;
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form2());


            database db = new database();
            data = db.datax();
            target = db.datay();

            foreach (var x in data[0])
            {
                Console.Write(x);
            }

        }
    }
}
