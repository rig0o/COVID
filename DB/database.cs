using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace COVID.DB
{
    public class database
    {
        private const string ConnectionString = @"Data Source=C:\SW\Dataset.db";
		public List<double[]> data;
		public List<double[]> target;
		public Dictionary<string, double> dataGrafico;
		public List<double[]> datax()
		{
			data = new List<double[]>();
			double[] entrada;
			SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
			string query = "SELECT * FROM DataSetMLP";
			SQLiteCommand comando = new SQLiteCommand(query,connect);
			SQLiteDataReader datos = comando.ExecuteReader();
			
			while(datos.Read())
            {
				entrada = new double[14];

				entrada[0] = datos.GetDouble(1);
				entrada[1] = datos.GetDouble(2);
				entrada[2] = datos.GetDouble(3);
				entrada[3] = datos.GetDouble(4);
				entrada[4] = datos.GetDouble(5);
				entrada[5] = datos.GetDouble(6);
				entrada[6] = datos.GetDouble(7);
				entrada[7] = datos.GetDouble(8);
				entrada[8] = datos.GetDouble(9);
				entrada[9] = datos.GetDouble(10);
				entrada[10] = datos.GetDouble(11);
				entrada[11] = datos.GetDouble(12);
				entrada[12] = datos.GetDouble(13);
				entrada[13] = datos.GetDouble(14);
				data.Add(entrada);
			}
			connect.Close();
			return data;
		}
		public List<double[]> datay()
		{
			target = new List<double[]>();
			double[] salida;

			SQLiteConnection connect = new SQLiteConnection(ConnectionString);
			connect.Open();
			string query = "SELECT * FROM DataSetMLP";
			SQLiteCommand comando = new SQLiteCommand(query, connect);
			SQLiteDataReader datos = comando.ExecuteReader();

			while (datos.Read())
			{
				salida = new double[10];
				salida[0] = datos.GetDouble(15);
				salida[1] = datos.GetDouble(16);
				salida[2] = datos.GetDouble(17);
				salida[3] = datos.GetDouble(18);
				salida[4] = datos.GetDouble(19);
				salida[5] = datos.GetDouble(20);
				salida[6] = datos.GetDouble(21);
				salida[7] = datos.GetDouble(22);
				salida[8] = datos.GetDouble(23);
				salida[9] = datos.GetDouble(24);
				target.Add(salida);
			}
			connect.Close();
			return target;

		}
		public SQLiteCommand tabla1()
        {
			SQLiteConnection connect = new SQLiteConnection(ConnectionString);
			string query = "SELECT Fecha, Casos_Nuevos_Totales from DataSetMLP";
			connect.Open();

			SQLiteCommand comando = new SQLiteCommand(query, connect);
			connect.Close();
			return comando;
        }
		public Dictionary<string,double> grafico1()
        {
			SQLiteConnection connect = new SQLiteConnection(ConnectionString);
			string query = "SELECT Fecha, Casos_Nuevos_Totales from DataSetMLP";
			connect.Open();
			SQLiteCommand comando = new SQLiteCommand(query, connect);
			SQLiteDataReader datos = comando.ExecuteReader();
			dataGrafico = new Dictionary<string, double>();
			while (datos.Read())
			{
				dataGrafico.Add(datos.GetString(0),datos.GetDouble(1));
			}
			connect.Close();
			return dataGrafico;
		}
		public double[] NormInverse(double[] x)
		{
			double[] aux = new double[x.Length];
			for (int i = 0; i < x.Length; i++)
			{
				aux[i] = x[i] * (385 - 4) + 4;  //return value * (max - min) + min;
			}
			return aux;
		}
		public double NormInverse(double x)
		{

			return	 Math.Round(x * (385 - 4) + 4);  //return value * (max - min) + min;

		}
		public List<double[]> iris()
		{
			data = new List<double[]>();
			double[] salida;

			SQLiteConnection connect = new SQLiteConnection(ConnectionString);
			connect.Open();
			string query = "SELECT * FROM Iris";
			SQLiteCommand comando = new SQLiteCommand(query, connect);
			SQLiteDataReader datos = comando.ExecuteReader();

			while (datos.Read())
			{
				salida = new double[4];
				salida[0] = datos.GetDouble(0);
				salida[1] = datos.GetDouble(1);
				salida[2] = datos.GetDouble(2);
				salida[3] = datos.GetDouble(3);
				data.Add(salida);
			}
			connect.Close();
			return data;

		}
	}
}
