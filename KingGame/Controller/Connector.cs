using KingGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingGame.Controller
{
    /// <summary>
    /// The class connects GUI with the mouse handler and other classes inside the Controller namespace
    /// </summary>
    class Connector: IMouseObserver
    {       
        public void AddObserver(IConnectorObserver observer)
        {
            observers.Add(observer);
        }

        public void Load(DiamondMatrix matrix)
        {
            this.matrix = matrix;
            gapsCreator = new GapsCreator(matrix);
        }

        public void OnSelect(int x, int y)
        {
            DiamondType type = matrix.Get(x, y);
            Select(x, y, type);
        }

        public void OnUndoSelection(int x, int y)
        {
            DiamondType type = matrix.Get(x, y);
            UndoSelection(x, y, type);
        }

        public void OnReplaceVertical(int x, int y1, int y2)
        {
            DiamondType type1 = matrix.Get(x, y1);
            DiamondType type2 = matrix.Get(x, y2);
            ReplaceVertical(x, y1, y2, type1, type2);
            ReplaceInMatrix(x, y1, x, y2);
            HashSet<Gap> gaps = gapsCreator.CreateGaps(x, y1, x, y2);
            if (gaps.Count == 0)
            {
                ReplaceVertical(x, y2, y1, type1, type2);
                ReplaceInMatrix(x, y2, x, y1);
            }
            else
            {
                ShowGaps(gaps.ToList());
                HandleFallingDiamonds(gaps);
            }
        }

        public void OnReplaceHorizontal(int y, int x1, int x2)
        {
            DiamondType type1 = matrix.Get(x1, y);
            DiamondType type2 = matrix.Get(x2, y);
            ReplaceHorizontal(y, x1, x2, type1, type2);
            ReplaceInMatrix(x1, y, x2, y);
            HashSet<Gap> gaps = gapsCreator.CreateGaps(x1, y, x2, y);
            if (gaps.Count == 0)
            {
                ReplaceHorizontal(y, x2, x1, type1, type2);
                ReplaceInMatrix(x2, y, x1, y);
            }
            else
            {
                ShowGaps(gaps.ToList());
                HandleFallingDiamonds(gaps);
            }
        }
        
        private void ReplaceInMatrix(int x1, int y1, int x2, int y2)
        {
            DiamondType type1 = matrix.Get(x1, y1);
            DiamondType type2 = matrix.Get(x2, y2);
            matrix.Set(x1, y1, type2);
            matrix.Set(x2, y2, type1);
        }

        private void Select(int x, int y, DiamondType type)
        {
            foreach (IConnectorObserver observer in observers)
            {
                observer.OnSelect(x, y, type);
            }
        }

        private void UndoSelection(int x, int y, DiamondType type)
        {
            foreach (IConnectorObserver observer in observers)
            {
                observer.OnUndoSelection(x, y, type);
            }
        }
       
        private void ReplaceVertical(int x, int y1, int y2, DiamondType type1, DiamondType type2)
        {
            foreach (IConnectorObserver observer in observers)
            {
                observer.OnReplaceVertical(x, y1, y2, type1, type2);
            }
        }

        private void ReplaceHorizontal(int y, int x1, int x2, DiamondType type1, DiamondType type2)
        {
            foreach (IConnectorObserver observer in observers)
            {
                observer.OnReplaceHorizontal(y, x1, x2, type1, type2);
            }
        }

        private void FellDown(IList<FallingColumn> list)
        {
            foreach (IConnectorObserver observer in observers)
            {
                observer.OnFellDown(list);
            }
        }

        private void ShowGaps(IList<Gap> gaps)
        {
            if (gaps.Count > 0)
            {
                foreach (IConnectorObserver observer in observers)
                {
                    observer.OnShowGaps(gaps);
                }
            }
        }

        private void HandleFallingDiamonds(HashSet<Gap> gaps)
        {
            UpdateMatrixWithGaps(gaps);
            FallingControl fallingDiamondsControl = new FallingControl(gaps, matrix);            
            IList<FallingColumn> fallingColumn = null;
            while (fallingDiamondsControl.DoCycle())
            {
                fallingColumn = fallingDiamondsControl.GetCurrentList();
                foreach (IConnectorObserver observer in observers)
                {
                    observer.OnFellDown(fallingColumn);
                }
                fallingDiamondsControl.CreateNewGapsAfterFall();
                HashSet<Gap> newGaps = fallingDiamondsControl.CreateNewGapsAfterFall();
                ShowGaps(newGaps.ToList());
            }
        }
       
        private void UpdateMatrixWithGaps(HashSet<Gap> gaps)
        {
            foreach (Gap gap in gaps)
            {
                matrix.Set(gap.X, gap.Y, DiamondType.Nothing);
            }           
        }

        private GapsCreator gapsCreator;
        private DiamondMatrix matrix;
        private IList<IConnectorObserver> observers = new List<IConnectorObserver>();        
    }
}
