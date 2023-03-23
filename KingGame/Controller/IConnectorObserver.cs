using KingGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Controller
{
    /// <summary>
    /// Allow other classes to subscribe to events of the Connector class.
    /// </summary>
    interface IConnectorObserver
    {
        void OnSelect(int x, int y, DiamondType diamondType);
        void OnUndoSelection(int x, int y, DiamondType diamondType);       
        void OnReplaceVertical(int x, int y1, int y2, DiamondType type1, DiamondType type2);
        void OnReplaceHorizontal(int y, int x1, int x2, DiamondType type1, DiamondType type2);
        void OnFellDown(IList<FallingColumn> list);
        void OnShowGaps(IList<Gap> gaps);
    }
}
