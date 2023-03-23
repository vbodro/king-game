using KingGame.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KingGame.GUI.GDI
{
    /// <summary>
    /// GDI implementation to paint diamonds and a board
    /// </summary>
    class DiamondScreen: IDiamondScreen
    {
        public DiamondScreen(BufferedGraphics buffGraphics)
        {
            this.buffGraphics = buffGraphics;
            backGroundmatrix = new BackGroundMatrix(buffGraphics);
        }

        public void Load(DiamondMatrix matrix)
        {
            for (int x = 0; x < Defs.xsize; x++)
            {
                for (int y = 0; y < Defs.ysize; y++)
                {
                    DiamondType diamondType = matrix.Get(x, y);
                    PutDiamond(x, y, diamondType);
                }
            }
        }     

        public void PutDiamond(int x, int y, DiamondType diamondType)
        {
            Bitmap diamondImg = diamondMap.Get(diamondType);
            PutDiamond(x, y, diamondImg);
        }

        public void OnSelect(int x, int y, DiamondType diamondType)
        {
            Bitmap diamondImg = diamondMap.GetSelected(diamondType);
            PutDiamond(x, y, diamondImg);
            buffGraphics.Render();
        }

        public void OnUndoSelection(int x, int y, DiamondType diamondType)
        {
            PutDiamond(x, y, diamondType);
            buffGraphics.Render();
        }

        public void OnReplaceVertical(int x, int y1, int y2, DiamondType type1, DiamondType type2)
        {                       
            int speed = Defs.movingSpeed;
            if (y1 > y2)
            {
                speed *= -1;
            }
            Replace(x, x, y1, y2, type1, type2, 0, speed);
        }
       
        public void OnReplaceHorizontal(int y, int x1, int x2, DiamondType type1, DiamondType type2)
        {
            int speed = Defs.movingSpeed;
            if (x1 > x2)
            {
                speed *= -1;
            }
            Replace(x1, x2, y, y, type1, type2, speed, 0);
        }

        public void OnFellDown(IList<FallingColumn> list)
        {            
            for (int counter = 0; counter <= Defs.movinSteps; counter++)
            {
                foreach (FallingColumn fd in list)
                {
                    PutFallingDiamonds(fd, counter);
                }
                buffGraphics.Render();
                Thread.Sleep(Defs.sleepingTime);
            }            
        }

        public void OnShowGaps(IList<Gap> gaps)
        {            
            foreach (Gap gap in gaps)
            {
                Bitmap diamondImg = diamondMap.GetRemoved(gap.Type);
                PutDiamond(gap.X, gap.Y, diamondImg);
                buffGraphics.Render();
            }
            buffGraphics.Render();
            Thread.Sleep(Defs.sleepingTime);

            foreach (Gap gap in gaps)
            {
                PutDiamond(gap.X, gap.Y, DiamondType.Nothing);
            }
            buffGraphics.Render();
            Thread.Sleep(Defs.sleepingTime);            
        }

        private void PutDiamond(int x, int y, Bitmap diamondImg)
        {
            int realX = Defs.xmargin + x * Defs.imgsize;
            int realY = Defs.ymargin + y * Defs.imgsize;            
            buffGraphics.Graphics.DrawImageUnscaled(backGroundmatrix.Get(x, y), realX, realY);
            if (diamondImg != null)
            {
                buffGraphics.Graphics.DrawImageUnscaled(diamondImg, realX, realY);
            }
        }

        private void Replace(int x1, int x2, int y1, int y2, DiamondType type1, DiamondType type2, int speedx, int speedy)
        {
            Bitmap img1 = diamondMap.Get(type1);
            Bitmap img2 = diamondMap.Get(type2);
            int realX1 = Defs.xmargin + x1 * Defs.imgsize;
            int realX2 = Defs.xmargin + x2 * Defs.imgsize;
            int realY1 = Defs.ymargin + y1 * Defs.imgsize;
            int realY2 = Defs.ymargin + y2 * Defs.imgsize;

            for (int counter = 0; counter <= Defs.movinSteps; counter++)
            {
                buffGraphics.Graphics.DrawImageUnscaled(backGroundmatrix.Get(x1, y1), realX1, realY1);
                buffGraphics.Graphics.DrawImageUnscaled(backGroundmatrix.Get(x2, y2), realX2, realY2);
                buffGraphics.Graphics.DrawImageUnscaled(img1, realX1 + speedx * counter, realY1 + speedy * counter);
                buffGraphics.Graphics.DrawImageUnscaled(img2, realX2 - speedx * counter, realY2 - speedy * counter);
                buffGraphics.Render();
                Thread.Sleep(Defs.sleepingTime);
            }
        }

        private void PutFallingDiamonds(FallingColumn fallingColumn, int counter)
        {
            int x = fallingColumn.X;
            int depth = fallingColumn.Depth;
            int y = -1 + depth;
            int realX = Defs.xmargin + x * Defs.imgsize;
            foreach (DiamondType diamond in fallingColumn.Diamonds)
            {                
                int realY = Defs.ymargin + y * Defs.imgsize;
                buffGraphics.Graphics.DrawImageUnscaled(backGroundmatrix.Get(x, y + 1), realX, realY + Defs.imgsize);
                Bitmap img = diamondMap.Get(diamond);
                if (img != null)
                {
                    buffGraphics.Graphics.DrawImageUnscaled(img, realX, realY + Defs.movingSpeed * counter);
                }
                y++;
            }
            buffGraphics.Graphics.DrawImageUnscaled(backGroundmatrix.Get(x, -1), realX, Defs.ymargin - Defs.imgsize);
            buffGraphics.Graphics.DrawImageUnscaled(backGroundmatrix.Get(x, -2), realX, Defs.ymargin - 2*Defs.imgsize);
        }

        private BufferedGraphics buffGraphics = null;
        private BackGroundMatrix backGroundmatrix = null;
        private DiamondMap diamondMap = new DiamondMap();
    }
}
