using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

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

        public void Draw(double x1, double y1, double x2, double y2, CanvasShape shape)
        {

        }

        public void Move(CanvasShape shape)
        {

        }

        public void Undo()
        {
            //Undo top action on actionsDone stack
            //Push undone action to actionsUndone stack
        }

        public void Redo()
        {
            //Redo top action on actionsUndone stack
            //Push redone action to actionsDone stack
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
