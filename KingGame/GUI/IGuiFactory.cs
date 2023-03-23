using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.GUI
{
    /// <summary>
    /// Abstract factory for Graphic classes
    /// </summary>
    interface IGuiFactory
    {
        IDiamondScreen CreateDiamondScreen();
        ITextPanel CreateTextPanel();
    }
}
