using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Common
{
    /// <summary>
    /// Contain a matrix with diamond types and a method to randomize it
    /// </summary>
    class DiamondMatrix
    {
        public DiamondType Get(int x, int y)
        {
            return matrix[x, y];
        }

        public void Set(int x, int y, DiamondType value)
        {
            matrix[x, y] = value;
        }

        public void Radomize()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            
            for (int x = 0; x < Defs.xsize; x++)
            {                
                for (int y = 0; y < Defs.ysize; y++)
                {
                    DiamondType newdiamond = DiamondType.Nothing;
                    DiamondType previousX = DiamondType.Nothing;
                    DiamondType previousY = DiamondType.Nothing;
                    if (x > 0) previousX = matrix[x - 1, y];
                    if (y > 0) previousY = matrix[x, y - 1];
                    while (newdiamond == DiamondType.Nothing || newdiamond == previousX || newdiamond == previousY)
                    {
                        newdiamond = (DiamondType)(rand.Next(Defs.diamondsCount) + 1);
                    }
                    
                    matrix[x, y] = newdiamond;                    
                }                
            }            
        }

        private DiamondType[,] matrix = new DiamondType[Defs.xsize, Defs.ysize];
    }
}
