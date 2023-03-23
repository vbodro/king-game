using KingGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Controller
{
    /// <summary>
    /// Used for testing if at particular position there is a diamond that is falling.
    /// For such a position a new gap must not be created.
    /// </summary>
    class FallingMatrix
    {
        public FallingMatrix()
        {
            for (int x = 0; x < Defs.xsize; x ++)
            {
                for (int y = 0; y < Defs.ysize; y++)
                {
                    matrix[x, y] = 0;
                }
            }
        }

        public void IncreaseSequence()
        {
            sequence++;
        }

        public void Set(int x, int y)
        {
            matrix[x, y] = sequence;
        }

        public bool Test(int x, int y)
        {
            return matrix[x, y] == sequence;
        }

        private int sequence = 0;
        private int[,] matrix = new int[Defs.xsize, Defs.ysize];
    }
}
