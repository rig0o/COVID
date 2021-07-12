using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID.SOM
{
    [Serializable]
    public class nodo
    {
        public double[] w;
        private int x;
        private int y;
        private int contador;
        public List<int> clasifica2;

        public nodo(int num, Random r)                    
        {
            clasifica2 = new List<int>();
            w = new double[num];
            for (int i = 0; i < num; i++)
                w[i] = r.NextDouble();  
        }
        #region Geter&Seter

        public int getX()
        {
            return x;
        }
        public void setX(int value)
        {
            x = value;
        }
        public int getY()
        {
            return y;
        }
        public void setY(int value)
        {
            y = value;
        }
        public double[] getW()
        {
            return w;
        }
        public void activacion()
        {
            contador += 1;
        }
        public int getContador()
        {
            return contador;
        }
        public double getContagios()
        {
            return w[2];
        }
        #endregion

        public void setPesos(double alfa, double theta, double[] input) // Se actualizan los pesos de la neuronas de la capa de salida
        {
            for (int i = 0; i < input.Length; i++)
            {
                this.w[i] += theta * alfa * (input[i] - this.w[i]);  // Ecuacion 3a peso anterior + theta*alfa*diferencia(vector actual y pesos)
            }
        }

        internal void clasificador(int k)
        {
            clasifica2.Add(k);
        }
    }
}
