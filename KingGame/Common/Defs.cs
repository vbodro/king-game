using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Common
{
    /// <summary>
    /// Definitions of all constant variables
    /// </summary>
    static class Defs
    {
        public const int imgsize = 36;
        public const int xsize = 10;
        public const int ysize = 10;
        public const int xmargin = 316;
        public const int ymargin = 94;
        public const int xmargin2 = xmargin + imgsize*xsize;
        public const int ymargin2 = ymargin + imgsize*ysize;
        public const int diamondsCount = 5;
        public const int movingSpeed = 2;
        public const int sleepingTime = 10;
        public const int movinSteps = imgsize / movingSpeed;
    }
}
