using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Common
{
    /// <summary>
    /// Used to allow correct work with the HashTable of Gaps
    /// </summary>
    class GapComparer: IEqualityComparer<Gap>
    {
        public bool Equals(Gap gap1, Gap gap2)
        {
            if (gap1.X == gap2.X && gap1.Y == gap2.Y)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Gap gap)
        {
            return gap.Y * 1000 + gap.X;
        }
    }
}
