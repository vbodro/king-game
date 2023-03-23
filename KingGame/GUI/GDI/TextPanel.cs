using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.GUI.GDI
{
    /// <summary>
    /// GDI+ implementation of a class to write a text on a panel
    /// </summary>
    class TextPanel: ITextPanel
    {
        public TextPanel(BufferedGraphics buffGraphics)
        {
            this.buffGraphics = buffGraphics;
            Bitmap backGroundImg = new Bitmap(ImagesResource.BackGround);
            scoreBackground = backGroundImg.Clone(new Rectangle(scoreLeft, scoreTop, scoreLeft + 100, scoreTop + 15), PixelFormat.DontCare);
            timeBackground = backGroundImg.Clone(new Rectangle(timeLeft, timeTop, timeLeft + 100, timeTop + 15), PixelFormat.DontCare);
        }
      
        public void OnSelect(int x, int y, Common.DiamondType diamondType)
        {
            
        }

        public void OnUndoSelection(int x, int y, Common.DiamondType diamondType)
        {
            
        }

        public void OnReplaceVertical(int x, int y1, int y2, Common.DiamondType type1, Common.DiamondType type2)
        {
            
        }

        public void OnReplaceHorizontal(int y, int x1, int x2, Common.DiamondType type1, Common.DiamondType type2)
        {
            
        }

        public void OnFellDown(IList<Common.FallingColumn> list)
        {
            
        }

        public void OnShowGaps(IList<Common.Gap> gaps)
        {
            sum += gaps.Count;
            WriteScore(sum);
        }

        public void WriteScore(int score)
        {
            buffGraphics.Graphics.DrawImageUnscaled(scoreBackground, scoreLeft, scoreTop);
            buffGraphics.Graphics.DrawString("Score: " + score, scoreFont, brush, new PointF(scoreLeft, scoreTop));
        }

        public void WriteTime(int time)
        {
            buffGraphics.Graphics.DrawImageUnscaled(timeBackground, timeLeft, timeTop);
            buffGraphics.Graphics.DrawString("Time: " + time, timeFont, brush, new PointF(timeLeft, timeTop));
            buffGraphics.Render();
        }

        private BufferedGraphics buffGraphics;
        private LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 0), new Point(3, 3), Color.White, Color.White);
        private Font scoreFont = new Font("Ariel", 20);
        private Font timeFont = new Font("Ariel", 18);
        private Bitmap scoreBackground;
        private Bitmap timeBackground;
        private int sum = 0;
        private const int scoreTop = 20;
        private const int scoreLeft = 20;
        private const int timeTop = 50;
        private const int timeLeft = 20;
    }
}
