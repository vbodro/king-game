using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Common
{
    /// <summary>
    /// Represents single position inside a matrix
    /// </summary>
    class Position
    {
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }        
        
        private int x;
        private int y;
    }
}
