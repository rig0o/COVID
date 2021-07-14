using System;
using System.Collections.Generic;

namespace COVID.SOM
{
    [Serializable]
    public class som
    {
        #region Parametros
        public matriz matriz;
        public som(matriz grid)
        {
            this.matriz = grid;
        }
        #endregion


        public void entrenar(List<double[]> datax, double alfa0, int iteracionMax)//  500 o  325 Filas 
        {
            Random r = new Random();
            for (int iteracion = 0; iteracion< iteracionMax; iteracion++)
            {
                double[] input = datax[r.Next(0, datax.Count - 1)];
                nodo bmu = matriz.getBMU(input);

                double sigma = Math.Pow(neighborhood(iteracion, iteracionMax), 2); 
                double alfa = learning(alfa0, iteracion, iteracionMax);

                for (int i = 0; i < matriz.ancho; i++)  
                {
                    for (int j = 0; j < matriz.alto; j++)
                    {
                        nodo nodoTemp = matriz.getNodo(i, j);
                        double theta = Math.Exp(-(matriz.distLateral(nodoTemp, bmu)) / (2 * sigma));
                        nodoTemp.setPesos(alfa, theta, input);
                    }
                }
                //Console.WriteLine("alfa->"+alfa+"--RO-->"+ neighborhood(iteracion, iteracionMax));  variable controladora

            }
        }
        public void clasificar(List<double[]> datax)
        {
            for (int k = 0; k < datax.Count; k++)
            {
                double[] input = datax[k];
                nodo bmu = matriz.getBMU(input);
                bmu.clasificador(k);
            }
        }
        public int radio0() //radio inical del mapa
        {
            return Math.Max(matriz.alto, matriz.ancho) / 2;
        }
        public double landa(int iteracionmax) // 2b Time Constant
        {
            return iteracionmax / Math.Log(radio0());     
        }
        public double neighborhood(int iteracion, int iteracionmax)   // Ecuacion 2a  radius of the neighborhood
        {
            return radio0() * Math.Exp(-iteracion / landa(iteracionmax)); 
        }
        public double learning(double alfa0, int iteracion, int iteracionmax)  // Ecuacion  3b con variante en la division
        {
            return alfa0 * Math.Exp(-iteracion / landa(iteracionmax)); 
        }
    }
}
