using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace DrawingApp
{
    public class CommandInvoker
    {
        private Stack<String> actionsDone, actionsUndone;
        private MainWindow mainWindow;

        public CommandInvoker(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        #region Drawing
        public void Draw(double x1, double y1, double x2, double y2, Rectangle shape)
        {
            throw new NotImplementedException();
        }
        
        public void Draw(Point p1, Point p2, Rectangle shape)
        {
            throw new NotImplementedException();
        }
        public void Draw(double x1, double y1, double x2, double y2, Ellipse shape)
        {
            throw new NotImplementedException();
        }
        
        public void Draw(Point p1, Point p2, Ellipse shape)
        {
            throw new NotImplementedException();
        }

        public void FinalizeDrawing()
        {
            throw new NotImplementedException();
        }
        #endregion

        public void Move(CanvasShape shape)
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            //Undo top action on actionsDone stack
            //Push undone action to actionsUndone stack
            throw new NotImplementedException();
        }

        public void Redo()
        {
            //Redo top action on actionsUndone stack
            //Push redone action to actionsDone stack
            throw new NotImplementedException();
        }

        internal static void Save()
        {
            throw new NotImplementedException();
        }

        internal static void Load()
        {
            throw new NotImplementedException();
        }

        public CanvasShape select(CanvasShape sender, MouseButtonEventArgs e)
        {
            if (mainWindow.selected != sender)
            {
                mainWindow.selected.Unselect();
                return sender.Select();
            }
            else
            {
                sender.Unselect();
                return null;
            }
        }
    }
}
