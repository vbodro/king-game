using KingGame.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.GUI
{
    /// <summary>
    /// Allow any graphics SDK to implement classes to write a text on a panel
    /// </summary>
    interface ITextPanel : IConnectorObserver
    {
        void WriteScore(int score);
        void WriteTime(int time);
    }
}
