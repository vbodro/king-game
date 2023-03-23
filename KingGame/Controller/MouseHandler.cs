using KingGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Controller
{
    /// <summary>
    /// Transfer mouse clicks to game commands
    /// </summary>
    class MouseHandler
    {
        public void OnClick(int x, int y)
        {
            if (x <= Defs.xmargin || y <= Defs.ymargin) return;
            if (x >= Defs.xmargin2 || y >= Defs.ymargin2) return;
            
            x = (x - Defs.xmargin) / Defs.imgsize;
            y = (y - Defs.ymargin) / Defs.imgsize;
            
            if (selected)
            {
                HandleDoubleSelection(x1, y1, x, y);                        
            }
            else
            {
                x1 = x;
                y1 = y;
                selected = true;
                OnSelect(x, y);
            }
        }

        public void AddObserver(IMouseObserver observer)
        {
            observers.Add(observer);
        }

        private void HandleDoubleSelection(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 || y1 == y2)
            {                
                if (Math.Abs(x1 - x2) == 1)
                {
                    OnReplaceHorizontal(y1, x1, x2);                    
                }
                else if (Math.Abs(y1 - y2) == 1)
                {
                    OnReplaceVertical(x1, y1, y2);
                }
                else
                {
                    OnUndoSelection(x1, y1);
                }                
            }
            else
            {
                OnUndoSelection(x1, y1);
            }
            selected = false;
        }

        private void OnSelect(int x, int y)
        {
            foreach (IMouseObserver observer in observers)
            {
                observer.OnSelect(x, y);
            }
        }

        private void OnUndoSelection(int x, int y)
        {
            foreach (IMouseObserver observer in observers)
            {
                observer.OnUndoSelection(x, y);
            }
        }

        private void OnReplaceHorizontal(int y, int x1, int x2)
        {
            foreach (IMouseObserver observer in observers)
            {
                observer.OnReplaceHorizontal(y, x1, x2);
            }
        }

        private void OnReplaceVertical(int x, int y1, int y2)
        {
            foreach (IMouseObserver observer in observers)
            {
                observer.OnReplaceVertical(x, y1, y2);
            }
        }
       
        private IList<IMouseObserver> observers = new List<IMouseObserver>();
        private bool selected = false;
        private int x1;
        private int y1;
    }
}
