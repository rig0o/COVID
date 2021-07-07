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
        public matriz(int num, int ancho, int alto) //Constructor de la clase Matriz(capa de salida).
        {
            this.ancho = ancho;
            this.alto = alto;
            grid = new nodo[this.ancho, this.alto];

            for (int i = 0; i < this.ancho; i++)
            {
                for (int j = 0; j < this.alto; i++)
                {
                    grid[i, j] = new nodo(num);
                    grid[i, j].setX(i);
                    grid[i, j].setY(j);
                }
            }
        }
        public nodo getNodo(int x, int y)
        {
            return grid[x, y];
        }
    }
}
