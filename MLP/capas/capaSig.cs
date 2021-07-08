using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID.MLP.capas
{
    [Serializable]
    public class CapaSig : Capa
    {
        public CapaSig(int num, int num_neuronas, Random r):base(num, num_neuronas, r)
        {
        }
        public override double funcion(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }
        public override double derivada(double input)
        {
            double y = funcion(input);
            return y * (1 - y);
        }
    }
}
