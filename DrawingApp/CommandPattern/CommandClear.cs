using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace DrawingApp.CommandPattern
{
    internal class CommandClear : Command
    {
        private readonly CommandInvoker invoker;

        public CommandClear(CommandInvoker invoker)
        {
            this.invoker = invoker;
        }

        public void Execute()
        {
            for (int i = invoker.MainWindow.canvas.Children.Count - 1; i > -1; i--)
            {
                invoker.Map.Remove((Shape)invoker.MainWindow.canvas.Children[i]);
                invoker.MainWindow.canvas.Children.RemoveAt(i);
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
