using COVID.DB;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using LiveCharts.Configurations;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COVID.MLP;

namespace COVID.Vista
{
    public partial class mlp : Form
    {
        database db;
        Mlp red;
        public mlp(database db, Mlp red)
        {
            this.db = db;
            this.red = red;
            InitializeComponent();
        }

        private void mlp_Load(object sender, EventArgs e)
        {

            Refresh();
            f5();
          
        }
        public void Refresh()
        {
            SQLiteCommand cmd = db.tabla1();
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            entrenamiento.fit();
            ////Init data
            //cartesianChart1.Series.Clear();
            //SeriesCollection series = new SeriesCollection();

            //Dictionary<string, double> data = db.grafico1();

            //List<double> valores = new List<double>();
            //List<string> fechas = new List<string>();

            //foreach(var i in data)
            //{
            //    fechas.Add(i.Key);
            //    valores.Add(db.NormInverse(i.Value));
            //}

            //series.Add(new LineSeries() { Title = "Contagios" ,Values = new ChartValues<double>(valores) });
            //cartesianChart1.Series = series;
            ////cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            ////{
            ////    Title = "Número de Días",
            ////    Labels = fechas.ToArray()

            ////});
            
        }
        public void f5()
        {
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            Dictionary<string, double> data = db.grafico1();
            List<double> valores = new List<double>();
            List<string> fechas = new List<string>();

            foreach (var i in data)
            {
                fechas.Add(i.Key);
                valores.Add(db.NormInverse(i.Value));
            }

            series.Add(new LineSeries() { Title = "Contagios", Values = new ChartValues<double>(valores) });
            cartesianChart1.Series = series;

            //cartesianChart1.Series = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Title= "contagios",
            //        Values = new ChartValues<DateTimePoint>()
            //        {
            //            new DateTimePoint(new DateTime(2008,1,2),2),
            //            new DateTimePoint(new DateTime(2008,1,3),4),
            //            new DateTimePoint(new DateTime(2008,1,4),6),
            //            new DateTimePoint(new DateTime(2008,1,5),8)
            //        }
            //    }
            //};
            //cartesianChart1.LegendLocation = LegendLocation.Right;
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            // seleccinar el ultimo registro de la base de datos
            double[] input= db.datax()[320];
            foreach (var x in input)
                Console.Write(x);
            double[] salida =db.NormInverse(red.Forward_propagation(input));


            // grid view 2 - Actualizacion
            int rowEscribir = dataGridView2.Rows.Count -1;
            //dataGridView2.Rows.Add();
            dataGridView2.Rows[rowEscribir].Cells[0].Value = salida[0];
            dataGridView2.Rows[rowEscribir].Cells[1].Value = salida[1];
            dataGridView2.Rows[rowEscribir].Cells[2].Value = salida[2];
            dataGridView2.Rows[rowEscribir].Cells[3].Value = salida[3];
            dataGridView2.Rows[rowEscribir].Cells[4].Value = salida[4];
            dataGridView2.Rows[rowEscribir].Cells[5].Value = salida[5];
            dataGridView2.Rows[rowEscribir].Cells[6].Value = salida[6];
            dataGridView2.Rows[rowEscribir].Cells[7].Value = salida[7];
            dataGridView2.Rows[rowEscribir].Cells[8].Value = salida[8];
            dataGridView2.Rows[rowEscribir].Cells[9].Value = salida[9];


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    
}
