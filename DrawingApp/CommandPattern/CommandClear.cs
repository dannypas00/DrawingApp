using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace DrawingApp.CommandPattern
{
    class CommandClear : Command
    {
        private CommandInvoker invoker;
        private Dictionary<Shape, CanvasShape> removed;

        public CommandClear(CommandInvoker invoker)
        {
            this.invoker = invoker;
        }

        public void Execute()
        {
            removed = invoker.map;
            for (int i = invoker.mainWindow.canvas.Children.Count - 1; i > -1; i--)
            {
                invoker.map.Remove((Shape)invoker.mainWindow.canvas.Children[i]);
                invoker.mainWindow.canvas.Children.RemoveAt(i);
            }
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            return;
        }
    }
}
