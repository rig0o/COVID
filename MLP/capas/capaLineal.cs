using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID.MLP.capas
{
    [Serializable]
    public class capaLineal : Capa
    {
        public capaLineal(int num, int num_neuronas, Random r) : base(num, num_neuronas, r)
        {
        }
        // f(x)=x;
        // derivada 1;
        public override double derivada(double x) 
        {
            return 1;
        }
        public override double funcion(double x)
        {
            return x;
        }
    }
}
