using KingGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Controller
{
    /// <summary>
    /// Used for creating of new gaps if a replace is made, or other event that 
    /// causes 3 or more the same diamonds in an array
    /// </summary>
    class GapsCreator
    {
        public GapsCreator(DiamondMatrix matrix)
        {
            this.matrix = matrix;            
        }

        public HashSet<Gap> CreateGaps(int x1, int y1, int x2, int y2)
        {            
            IList<Position> positions = new List<Position>();
            positions.Add(new Position(x1, y1));
            positions.Add(new Position(x2, y2));

            return CreateGaps(positions);
        }

        public HashSet<Gap> CreateGaps(IList<Position> positions)
        {
            HashSet<Gap> gapSet = new HashSet<Gap>(new GapComparer());            
            foreach (Position position in positions)
            {
                DiamondType type = matrix.Get(position.X, position.Y);
                IList<Gap> horizontalGaps = CreateHorizontalGaps(position.X, position.Y, type);
                if (horizontalGaps.Count > 2) InsertToSet(gapSet, horizontalGaps);
                IList<Gap> verticalGaps = CreateVerticalGaps(position.X, position.Y, type);
                if (verticalGaps.Count > 2) InsertToSet(gapSet, verticalGaps);
            }

            return gapSet;
        }

        public HashSet<Gap> CreateNewGapsAfterFall(HashSet<Gap> consumedGaps)
        {
            HashSet<Gap> newGaps = new HashSet<Gap>(new GapComparer());
            foreach (Gap gap in consumedGaps)
            {
                if (gap.Y < Defs.ysize - 1 && matrix.Get(gap.X, gap.Y + 1) != DiamondType.Nothing)
                {
                    List<Position> positions = new List<Position>();
                    for (int y = 0; y <= gap.Y; y++)
                    {
                        positions.Add(new Position(gap.X, y));
                    }                    
                    foreach (Gap g in CreateGaps(positions))
                    {
                        newGaps.Add(g);
                    }
                }
            }
            return newGaps;
        }

        public HashSet<Gap> FindGapsInFeltColumns(HashSet<Gap> consumedGaps)
        {
            HashSet<Gap> gapSet = new HashSet<Gap>(new GapComparer());
            foreach (Gap gap in consumedGaps)
            {
                for (int y = 0; y < Defs.ysize; y++)
                {
                    if (matrix.Get(gap.X, y) == DiamondType.Nothing)
                    {
                        gapSet.Add(new Gap(gap.X, y, DiamondType.Nothing));
                    }
                }
            }
            return gapSet;
        }


        private void InsertToSet(HashSet<Gap> gapSet, IList<Gap> gaps)
        {
            foreach (Gap gap in gaps)
            {
                if (! gapSet.Contains(gap))
                {
                    gapSet.Add(gap);
                }
            }
        }

        private IList<Gap> CreateHorizontalGaps(int x, int y, DiamondType type)
        {
            IList<Gap> gaps = new List<Gap>();
            gaps.Add(new Gap(x, y, type));
            int x2 = x + 1;
            while (x2 < Defs.xsize && matrix.Get(x2, y) == type)
            {
                gaps.Add(new Gap(x2, y, type));
                x2++;
            }
            x2 = x - 1;
            while (x2 >= 0 && matrix.Get(x2, y) == type)
            {
                gaps.Add(new Gap(x2, y, type));
                x2--;
            }
            return gaps;
        }

        private IList<Gap> CreateVerticalGaps(int x, int y, DiamondType type)
        {
            IList<Gap> gaps = new List<Gap>();
            gaps.Add(new Gap(x, y, type));
            int y2 = y + 1;
            while (y2 < Defs.ysize && matrix.Get(x, y2) == type)
            {
                gaps.Add(new Gap(x, y2, type));
                y2++;
            }
            y2 = y - 1;
            while (y2 >= 0 && matrix.Get(x, y2) == type)
            {
                gaps.Add(new Gap(x, y2, type));
                y2--;
            }
            return gaps;
        }
        
        private DiamondMatrix matrix;
    }
}
