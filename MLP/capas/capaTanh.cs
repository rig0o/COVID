using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID.MLP.capas
{
    [Serializable]
    public class capaTanh : capa
    {
        public CapaTanh(int num, int num_neuronas, Random r) : base(num, num_neuronas, r)
        {
        }
        public override double derivada(double x)
        {
            return 1 - Math.Pow(funcion(x), 2);
        }
        public override double funcion(double x)
        {
            return 2 / (1 + Math.Pow(Math.E, -(2 * x))) - 1;
        }
    }
}
