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

        public nodo(int num)                      //Constructor de neurona, inicializa los pesos de cada neurona
        {
            Random r = new Random();              //inicializar los pesos de la red neuronal
            w = new double[num];
            for (int i = 0; i < num; i++)
                w[i] = 2 * r.NextDouble() - 1;   // Rango en el cual se inician los pesos -0.5 a 0.5
        }
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
    }
}
