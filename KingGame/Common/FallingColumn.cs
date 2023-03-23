using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Common
{
    /// <summary>
    /// Represent a list of diamonds that are falling down because of a gap bellow them.
    /// The Depth property is used if a column is falling from a middle of a matrix
    /// </summary>
    class FallingColumn
    {        
        public IList<DiamondType> Diamonds
        {
            get { return diamonds; }
            set { diamonds = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        private int x;
        private int depth = 0;
        private IList<DiamondType> diamonds = new List<DiamondType>();
    }
}
