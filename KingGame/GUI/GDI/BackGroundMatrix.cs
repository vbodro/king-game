using KingGame.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.GUI.GDI
{
    /// <summary>
    /// Contains a background for each position inside a matrix. It also allow indexing with negative
    /// numbers to provide more natural usage. GDI+ implementation.
    /// </summary>
    class BackGroundMatrix
    {
        public BackGroundMatrix(BufferedGraphics graphics)
        {
            Bitmap backGroundImg = new Bitmap(ImagesResource.BackGround);
            graphics.Graphics.DrawImageUnscaled(backGroundImg, 0, 0);
            for (int x = 0; x < Defs.xsize; x++)
            {
                for (int y = -2; y < Defs.ysize; y++)
                {
                    int yy = y;
                    if (y < 0)
                    {
                        yy = Defs.ysize - y - 1;
                    }
                    matrix[x, yy] = backGroundImg.Clone(new Rectangle(Defs.xmargin + x * Defs.imgsize, Defs.ymargin + y * Defs.imgsize, Defs.imgsize, Defs.imgsize), PixelFormat.DontCare);
                }
            }
        }
       

        public Bitmap Get(int x, int y)
        {
            if (y >= 0)
            {
                return matrix[x, y];
            }
            else
            {
                return matrix[x, Defs.ysize - y - 1];
            }
        }


        private Bitmap[,] matrix = new Bitmap[Defs.xsize, Defs.ysize + 2];
    }
}
