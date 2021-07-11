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
using System.Drawing.Printing;

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
            EntrenarSom.fit();
            mapa = EntrenarSom.carga();
            InitializeComponent();
            refresh();
        }
        #region f5
        public void refresh()
        {
            var r = new Random();
            puntos = new List<HeatPoint>();
            puntos2 = new List<HeatPoint>();
            for (int i = 0; i < mapa.matriz.ancho; i++)
            {
                for (int j = 0; j < mapa.matriz.alto; j++)
                {
                    puntos2.Add(new HeatPoint(i, j, mapa.matriz.grid[i, j].getContagios()));
                    puntos.Add(new HeatPoint(i, j, mapa.matriz.grid[i, j].getContador()));
                }
            }

            cartesianChart2.Series.Add(new HeatSeries
            {
                Title = "Neurona",
                Values = new ChartValues<HeatPoint>(puntos2),
                DataLabels = false
            });
            cartesianChart1.Series.Add(new HeatSeries
            {
                Title = "Neurona",
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
        void ImprimirForm(object o, PrintPageEventArgs e)               ///Función Imprimir Form
        {
            int x = SystemInformation.WorkingArea.X;
            int y = SystemInformation.WorkingArea.Y;
            int ancho = this.Width;
            int alto = this.Height;

            Rectangle bounds = new Rectangle(x, y, ancho, alto);

            Bitmap img = new Bitmap(ancho, alto);

            this.DrawToBitmap(img, bounds);
            Point p = new Point(100, 100);
            e.Graphics.DrawImage(img, p);
        }
        #endregion

        #region Graficos
        private void cartesianChart2_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        #endregion

        #region Botones
        private void button1_Click(object sender, EventArgs e) // exportar
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(ImprimirForm);
                pd.DefaultPageSettings.Landscape = true;              ///Exportar pdf Horizontalmente
                MessageBox.Show("Preparando archivo a exportar, seleccione destino");
                pd.Print();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)      //btnVolver
        {
            this.SetVisibleCore(false);
            new principal().ShowDialog();
            this.Dispose();
        }
        #endregion

        private void som_Load(object sender, EventArgs e)
        {

        }
    }
}
