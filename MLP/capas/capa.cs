using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID.MLP.capas
{
    [Serializable]
    public abstract class capa
    {
        public List<neurona> neuronas;
        //public double[] output;

        public capa(int conexiones, int neuronas, Random r)
        {
            this.neuronas = new List<neurona>();
            for (int i = 0; i < this.neuronas.Count; i++)
                this.neuronas.Add(new neurona(conexiones, r));
        }
        public double[] Activacion(double[] input)             ////activación de todas las neuronas de una sola capa
        {
            List<double> outputs = new List<double>();
            for (int i = 0; i < neuronas.Count; i++)
            {
                outputs.Add(funcion(neuronas[i].suma(input)));
            }
            //output = outputs.ToArray();
            return outputs.ToArray();
        }
        public abstract double funcion(double input);

        public abstract double derivada(double input);
    }
}
