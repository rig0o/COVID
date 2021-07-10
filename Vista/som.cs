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

namespace COVID.Vista
{
    public partial class som : Form
    {
        List<HeatPoint> puntos;
        public som()
        {
            InitializeComponent();
            refresh();
        }
        public void refresh()
        {
            var r = new Random();
            puntos = new List<HeatPoint>();
            int m = 10;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                    puntos.Add(new HeatPoint(i, j, r.Next(0, 100)));

            cartesianChart1.Series.Add(new HeatSeries
            {
                Title = "Neurna",
                Values = new ChartValues<HeatPoint>(puntos),
                DataLabels = true
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
    }
}
