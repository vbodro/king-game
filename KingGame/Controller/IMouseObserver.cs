using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Controller
{
    /// <summary>
    /// Allow other classed to subscribe to an event of the mouse handler
    /// </summary>
    interface IMouseObserver
    {
        void OnSelect(int x, int y);
        void OnReplaceVertical(int x, int y1, int y2);
        void OnReplaceHorizontal(int y, int x1, int x2);
        void OnUndoSelection(int x, int y);
    }
}
