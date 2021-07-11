using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID.SOM
{
    [Serializable]
    public class matriz
    {
        public nodo[,] grid;
        public int ancho;
        public int alto;
        public matriz(int num, int ancho, int alto,Random r) //Constructor de la clase Matriz(capa de salida).
        {
            this.ancho = ancho;
            this.alto = alto;
            grid = new nodo[this.ancho, this.alto];

            for (int i = 0; i < this.ancho; i++)
            {
                for (int j = 0; j < this.alto; j ++)
                {
                    grid[i, j] = new nodo(num,r);
                    grid[i, j].setX(i);
                    grid[i, j].setY(j);
                }
            }
        }
        public nodo getNodo(int x, int y)
        {
            return grid[x, y];
        }
        public nodo getBMU(double[] input) 
        {
            nodo bmu = this.grid[0,0];
            double mejorDist = distEuclediana(bmu.getW(), input);
            double dist;

            for (int i = 0; i < this.ancho; i++)
            {
                for (int j = 0; j < this.alto; j++)
                {
                    nodo compara = this.grid[i, j];
                    dist = distEuclediana(compara.getW(), input);
                    //Console.WriteLine(dist+"<"+mejorDist);
                    if (dist < mejorDist)
                    {
                        bmu = this.grid[i, j];
                        mejorDist = dist;
                    }
                }
            }
            bmu.activacion();
            return bmu;
        }
        public static double distEuclediana(double[] vector1, double[] vector2) 
        {
            if (vector1.Length != vector2.Length)
            {
                return -1;
            }
            double sum = 0;
            for (int i = 0; i < vector1.Length; i++)
            {
                sum += Math.Pow(vector1[i] - vector2[i], 2);
            }
            return sum;
        }
        public static double distLateral(nodo nodo1, nodo nodo2) // Distancia lateral entre dos neuronas (nodos)
        {
            double dif1 = Math.Pow(nodo1.getX() - nodo2.getX(), 2);      //Diferencia en valor absoluto
            double dif2 = Math.Pow(nodo1.getY() - nodo2.getY(), 2);
            return dif1 + dif2;
        }
    }
}
