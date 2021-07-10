using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COVID.SOM;
using COVID.DB;

namespace COVID.Vista
{
    public partial class som : Form
    {
        public COVID.SOM.som mapa;
        public List<double[]> data;
        List<HeatPoint> puntos;
        List<HeatPoint> puntos2;
        public som()
        {
            //EntrenarSom.fit();
            mapa = EntrenarSom.carga();
            InitializeComponent();
            refresh();
        }
        public void refresh()
        {
            var r = new Random();
            puntos = new List<HeatPoint>();
            puntos2 = new List<HeatPoint>();
            for (int i = 0; i < mapa.matriz.ancho; i++)
                for (int j = 0; j < mapa.matriz.alto; j++)
                {
                    puntos2.Add(new HeatPoint(i, j, mapa.matriz.grid[i, j].getContagios()));
                    puntos.Add(new HeatPoint(i, j, mapa.matriz.grid[i, j].getContador()));
                }

            //int m=10;
            //for (int i = 0; i < m; i++)
            //    for (int j = 0; j < m; j++)
            //        puntos.Add(new HeatPoint(i, j, r.Next(0, 100)));

            cartesianChart2.Series.Add(new HeatSeries
            {
                Title = "Neurna",
                Values = new ChartValues<HeatPoint>(puntos2),
                DataLabels = false
            });
            cartesianChart1.Series.Add(new HeatSeries
            {
                Title = "Neurna",
                Values = new ChartValues<HeatPoint>(puntos),
                DataLabels = false
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                //LabelsRotation = -15,
                Labels = new[]
                {
                    "1",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7",
                    "8",
                    "9",
                    "10"
                },
                Separator = new Separator { Step = 1 }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Labels = new[]
                {
                    "1",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7",
                    "8",
                    "9",
                    "10"
                },
                Separator = new Separator { Step = 1 }
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void cartesianChart2_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
