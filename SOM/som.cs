using System;
using System.Collections.Generic;

namespace COVID.SOM
{
    [Serializable]
    public class som
    {
        public matriz matriz;
        public som(matriz grid)
        {
            this.matriz = grid;
        }
        public void entrenar(List<double[]> datax, double alfa, int epoch)
        {

            for (int iteracion = epoch; iteracion > 0; iteracion--)
            {
                for (int k = 0; k < datax.Count; k++)                   //recorre la data
                {
                    double[] input = datax[k];
                    nodo bmu = getBMU(datax[k]);

                    for (int i = 0; i < matriz.ancho; i++)              //recorre para actualizar los pesos
                    {
                        for (int j = 0; j < matriz.alto; j++)
                        {
                            nodo nodoTemp = matriz.getNodo(i, j);
                            setPesos(nodoTemp, bmu, alfa, iteracion, epoch, input);
                        }
                    }
                }
            }
        }

        public nodo getBMU(double[] input) //calcula la distancia del vector entrante por cada uno de los nodos de la matriz y se va guardando el nodo que tenga el mejor valor y una vez terminado de recorrer todos los elementos de la matriz este nodo es retornado
        {
            nodo bmu = matriz.getNodo(0, 0);
            double mejorDist = distEuclediana(bmu.getW(), input);
            double dist;

            for (int i = 0; i < matriz.ancho; i++)
            {
                for (int j = 0; j < matriz.alto; j++)
                {
                    nodo compara = matriz.getNodo(i, j);
                    dist = distEuclediana(compara.getW(), input);
                    if (dist < mejorDist)
                    {
                        bmu = matriz.getNodo(i, j);
                        mejorDist = dist;
                    }
                }
            }
            return bmu;
        }
        public void setPesos(nodo nodo, nodo BMU, double alfa0, int actual, int epoc, double[] input) // Se actualizan los pesos de la neuronas de la capa de salida
        {
            for (int i = 0; i < input.Length; i++)
            {
                double sigma = Math.Pow(radio(actual, epoc), 2);  // lo puse aca para que no quedara un codigo muy largo abajo
                double theta = Math.Exp(-(distLateral(nodo, BMU)) / (2 * sigma));
                double alfa = learning(alfa0, actual, epoc);

                nodo.w[i] += nodo.w[i] * theta * alfa * (input[i] - nodo.w[i]);  // Ecuacion 3a peso anterior + theta*alfa*diferencia(vector actual y pesos)
            }
        }
        public static double distEuclediana(double[] vector1, double[] vector2) // Calcula la similitud entre dos vectores de entrada (típicamente un input y los pesos de una neurona).
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
        public int radio0() //radio inical, se utilizara para calcular landa
        {
            return Math.Max(matriz.alto, matriz.ancho) / 2;
        }
        public double landa(int epoc) // constante landa  --> epoc= iteraciones a realizar .
        {
            return epoc / Math.Log(radio0()); // numero iteraciones/radioinicial --> version tesis
                                              //return epoc/radio0();                                            --> version paper
        }
        public double radio(int actual, int epoc)   // Ecuacion 2a  radius of the neighborhood
        {                                           // actual = iteracion actual; epoc = numero de iteraciones a realizar 
            return radio0() * Math.Exp(-actual / landa(epoc));
        }
        public double learning(double alfa0, int actual, int epocas)  // Ecuacion  3b con variante en la division
        {
            return alfa0 * Math.Exp(-actual / epocas);        // L0, alfa0 inicial * exp(-iteracion/epocas) --> version tesis
                                                              //return alfa0 * Math.Exp(-actual / landa());   //L0, alfa0 inicial * exp(-iteracion/landa) -->  version paper 
        }
    }
}
