using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID.MLP
{
    [Serializable]
    public class neurona
    {
        public double[] w { get; set; }
        public double b { get; set; }
        public double ultimaActivacion;

        public neurona(int num, Random r)
        {
            b = (10 * r.NextDouble()) - 5;
            w = new double[num];
            for (int i = 0; i < num; i++)
                w[i] = 10 * r.NextDouble() - 5;
        }
        /// <summary>
        /// Suma ponderada
        /// </summary>
        public double suma(double[] inputs)//suma ponderada   z = x*w + b
        {
            double sum = b;
            for (int i = 0; i < inputs.Length; i++)
                sum += inputs[i] * w[i];

            ultimaActivacion = sum;
            return sum;
        }
    }
}
