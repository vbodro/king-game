using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.GUI.GDI
{
    /// <summary>
    /// GDI+ implementation of abstract factory for Graphic classes
    /// </summary>
    class GuiFactoryGDI: IGuiFactory
    {
        public GuiFactoryGDI(BufferedGraphics buffGraphics)
        {
            this.buffGraphics = buffGraphics;
        }

        public IDiamondScreen CreateDiamondScreen()
        {
            return new DiamondScreen(buffGraphics);
        }

        public ITextPanel CreateTextPanel()
        {
            return new TextPanel(buffGraphics);
        }

        private BufferedGraphics buffGraphics = null;
    }
}
