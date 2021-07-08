using COVID.MLP.capas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID.MLP // VERSION ANTIGUA SOLO MLP
{
    [Serializable]
    public class Mlp 
    {
        public List<Capa> capas;
        public List<double[]> sigma;
        public List<double[,]> deltas;
        public Mlp(List<Capa> layers) // la longitud del vector indica la cantida de capas , y los valores idican la cantidad de neuronas por cada capa
        {
            capas = new List<Capa>();
            capas = layers; //capas
        }

        public double[] Forward_propagation(double[] entrada) //Fordward pass  activación de todas las capas
        {
            double[] salida = new double[0];
            for (int i = 0; i < capas.Count; i++)
            {
                salida = capas[i].Activacion(entrada);
                entrada = salida;
            }
            return salida;
        }
        public bool Entrenar(List<double[]> entradas, List<double[]> salidas, double alfa, double maxError, int maxIteracion)
        {
            double error = 99999;
            while (error > maxError)
            {
                maxIteracion--;
                if (maxIteracion <= 0)
                {
                    Console.WriteLine("---------------------Minimo local-------------------------");
                    Console.WriteLine(error);
                    return false;
                }

                Backpropagation(entradas, salidas, alfa);
                error = ErrorGeneral(entradas, salidas);
                //if (maxIteracion % 100 == 0)
                    Console.WriteLine(error + " ------" + maxIteracion);
            }

            return true;
        }
        public void Backpropagation(List<double[]> entradas, List<double[]> salidas, double alfa)
        {
            ResetearDeltas();
            for (int i = 0; i < entradas.Count; i++)
            {
                Forward_propagation(entradas[i]); //el vector de entrda i-esimo se propa por la red
                SetSigma(salidas[i]);   //atraves de la salida esperada se calcula los valores sigma derivadas
                SetBias(alfa);  // actualizamos los valores bias
                SetDelta();
            }
            SetPesos(alfa); //actulizamos los pesos
        }
        public void SetSigma(double[] output)
        {
            sigma = new List<double[]>();
            for (int i = 0; i < capas.Count; i++)
            {
                sigma.Add(new double[capas[i].neuronas.Count]);
            }

            for (int i = capas.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < capas[i].neuronas.Count; j++)
                {
                    if (i == capas.Count - 1)
                    {
                        double y = capas[i].neuronas[j].ultimaActivacion; // ultima activacion
                        sigma[i][j] = (capas[i].funcion(y) - output[j]) * capas[i].derivada(y);
                    }
                    else
                    {
                        double sum = 0;
                        for (int k = 0; k < capas[i + 1].neuronas.Count; k++)
                        {
                            sum += capas[i + 1].neuronas[k].w[j] * sigma[i + 1][k];
                        }
                        sigma[i][j] = capas[i].derivada(capas[i].neuronas[j].ultimaActivacion) * sum;
                    }
                }
            }
        }
        public void SetPesos(double alfa)
        {
            for (int i = 0; i < capas.Count; i++)
            {
                for (int j = 0; j < capas[i].neuronas.Count; j++)
                {
                    for (int k = 0; k < capas[i].neuronas[j].w.Length; k++)
                    {
                        capas[i].neuronas[j].w[k] -= alfa * deltas[i][j, k];
                        Console.Write();
                    }

                }
            }
        }
        public void SetBias(double alfa)
        {
            for (int i = 0; i < capas.Count; i++)
            {
                for (int j = 0; j < capas[i].neuronas.Count; j++)
                {
                    capas[i].neuronas[j].b -= alfa * sigma[i][j];
                }
            }
        }
        public void ResetearDeltas()
        {
            deltas = new List<double[,]>(); //Lista de un vector de 2 dimenciones por cada capa, [num neuronas, num entrdas de la neurona] 
            for (int i = 0; i < capas.Count; i++)
            {
                deltas.Add(new double[capas[i].neuronas.Count, capas[i].neuronas[0].w.Length]);
            }
        }
        public void SetDelta()
        {
            for (int i = 1; i < capas.Count; i++)
            {
                for (int j = 0; j < capas[i].neuronas.Count; j++)
                {
                    for (int k = 0; k < capas[i].neuronas[j].w.Length; k++)
                    {
                        deltas[i][j, k] += sigma[i][j] * capas[i - 1].funcion(capas[i - 1].neuronas[k].ultimaActivacion);

                    }
                }
            }
        }
        public double ErrorIndividual(double[] realOutput, double[] output) //Se debe Cuantificar el error, en este caso para una salida en especifico
        {
            double error = 0;
            for (int i = 0; i < realOutput.Length; i++)
            {
                error += Math.Pow(realOutput[i] - output[i], 2); //error cuadratico medio, elevamos al cuadrado la diferencia entre el valor obtenido y el valor que deseamos 
            }
            error = error / realOutput.Length;
            return error;
        }
        public double ErrorGeneral(List<double[]> inputs, List<double[]> outputs) //Se debe Cuantificar el error, en este caso para todas las salidas juntas
        {
            double error = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                error += ErrorIndividual(Forward_propagation(inputs[i]), outputs[i]); //ErrorIndividual, salidas y se compara con la salida deseada
            }
            error = error / inputs.Count;
            return error;
        }

    }
}
