using KingGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Controller
{
    /// <summary>
    /// The class controls a creation of falling columns and new gaps as result of fallen diamonds
    /// </summary>
    class FallingControl
    {
        public FallingControl(HashSet<Gap> gaps, DiamondMatrix matrix)
        {
            this.gaps = gaps;
            this.matrix = matrix;
            gapsCreator = new GapsCreator(matrix);
            this.random = new Random(DateTime.Now.Millisecond);
        }

        public bool DoCycle()
        {
            if (gaps.Count > 0)
            {
                fallingMatrix.IncreaseSequence();
                HashSet<Gap> newGaps = new HashSet<Gap>();
                consumedGaps = new HashSet<Gap>(new GapComparer());
                foreach (Gap gap in gaps)
                {
                    if (gap.Y == 0 || matrix.Get(gap.X, gap.Y - 1) != DiamondType.Nothing)
                    {
                        consumedGaps.Add(gap);
                    }
                    else
                    {
                        newGaps.Add(gap);
                    }
                }
                gaps = newGaps;
                return true;
            }
            return false;
        }

        public IList<FallingColumn> GetCurrentList()
        {            
            List<FallingColumn> fallingColumns = new List<FallingColumn>();           
            foreach (Gap gap in consumedGaps)
            {
                FallingColumn fallColumn = CreateFallingDiamonds(gap.X, gap.Y);
                UpdateMatrix(fallColumn, gap.X);
                fallingColumns.Add(fallColumn);
            }
            return fallingColumns;
        }

        public HashSet<Gap> CreateNewGapsAfterFall()
        {
            HashSet<Gap> newGaps = gapsCreator.CreateNewGapsAfterFall(consumedGaps);
            AddNewGaps(newGaps);
            HashSet<Gap> foundGaps = gapsCreator.FindGapsInFeltColumns(consumedGaps);
            UpdateMatrixWithGaps(newGaps);
            AddNewGaps(foundGaps);
            return newGaps;
        }
               
        private void AddNewGaps(HashSet<Gap> newGaps)
        {                      
            List<Gap> toDeleteGaps = new List<Gap>();
            foreach (Gap newGap in newGaps)
            {
                if (!(gaps.Contains(newGap) || fallingMatrix.Test(newGap.X, newGap.Y)))
                {
                    gaps.Add(newGap);
                }
                else
                {
                    toDeleteGaps.Add(newGap);
                }
            }
            foreach (Gap gap in toDeleteGaps)
            {
                newGaps.Remove(gap);
            }
        }
       
        private FallingColumn CreateFallingDiamonds(int x, int y)
        {
            FallingColumn fallingColumn = new FallingColumn();
            fallingColumn.X = x;
            DiamondType newType;
            DiamondType previous = matrix.Get(x, 0);
            while ((newType = (DiamondType)(random.Next(Defs.diamondsCount) + 1)) == previous);
            fallingColumn.Diamonds.Add(newType);
            for (int yy = 0; yy < y; yy++)
            {                
                if (matrix.Get(x, yy) == DiamondType.Nothing)
                {
                    fallingColumn.Diamonds.Clear();
                    fallingColumn.Diamonds.Add(DiamondType.Nothing);
                    while (yy < y && matrix.Get(x, yy) == DiamondType.Nothing) yy++;
                    fallingColumn.Depth = yy;
                }
                fallingColumn.Diamonds.Add(matrix.Get(x, yy));                
            }
           
            return fallingColumn;
        }

        private void UpdateMatrix(FallingColumn fallingColumn, int x)
        {
            int y = fallingColumn.Depth;
            int depth2 = fallingColumn.Depth + fallingColumn.Diamonds.Count;
            foreach (DiamondType type in fallingColumn.Diamonds)
            {
                matrix.Set(x, y, type);
                if (depth2 < Defs.ysize && matrix.Get(x, depth2) == DiamondType.Nothing)
                {
                    fallingMatrix.Set(x, y);
                }
                y++;
            }
        }
       
        private void UpdateMatrixWithGaps(HashSet<Gap> gaps)
        {
            foreach (Gap gap in gaps)
            {
                matrix.Set(gap.X, gap.Y, DiamondType.Nothing);
            }
        }

        private FallingMatrix fallingMatrix = new FallingMatrix();
        private DiamondMatrix matrix;
        private HashSet<Gap> gaps;
        HashSet<Gap> consumedGaps;        
        GapsCreator gapsCreator;
        private Random random;       
    }
}
