using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID.MLP.capas
{
    [Serializable]
    public class capaRelu : capa
    {
        public capaRelu(int num, int num_neuronas, Random r) : base(num, num_neuronas, r)
        {
        }
        public override double derivada(double x)
        {
            //return Math.Max(0, 1);// x < 0 ? 0 : x;
            return x < 0 ? 0.01 : 1;  //Leaky RELU
            //return x < 0 ? 0 : 1;
        }
        public override double funcion(double x)
        {
            //return Math.Max(0, x);// x < 0 ? 0 : x;
            return x < 0 ? 0.01 * x : x;  //LEAKY RELU
            //return x <= 0 ? 0 : x;
        }
    }
}
