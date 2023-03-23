using KingGame.Common;
using KingGame.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.GUI
{
    /// <summary>
    /// Allow any graphics SDK to implement classes paint diamonds and a board
    /// </summary>
    interface IDiamondScreen: IConnectorObserver
    {        
        void Load(DiamondMatrix matrix);        
    }
}
