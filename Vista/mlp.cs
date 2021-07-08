using COVID.DB;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVID.Vista
{
    public partial class mlp : Form
    {
        database db;
        public mlp(database db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mlp_Load(object sender, EventArgs e)
        {

            Refresh();
          
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
            //Init data
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            Dictionary<string, double> data = db.grafico1();

            List<double> valores = new List<double>();
            List<string> fechas = new List<string>();

            foreach(var i in data)
            {
                fechas.Add(i.Key);
                valores.Add(i.Value);
            }

 
            series.Add(new LineSeries() { Title = "contaigos" ,Values = new ChartValues<double>(valores) });

            cartesianChart1.Series = series;

            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Número de Días",
                Labels = fechas.ToArray()

            });
            //cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            //{
            //    Title = "Número de Contagiados",
            //    //LabelFormatter = value => value.ToString("C")
            //    //LabelFormatter = 
            //});

            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
        }
    }
}
