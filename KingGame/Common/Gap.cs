using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Common
{
    /// <summary>
    /// A single gap that apears when an array of 3 or more the same diamonds is destroyed
    /// </summary>
    class Gap
    {
        public Gap(int x, int y, DiamondType type)
        {        
            this.x = x;
            this.y = y;
            this.type = type;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public DiamondType Type { get { return type; } }
        
        private int x;
        private int y;
        private DiamondType type;
    }
}
