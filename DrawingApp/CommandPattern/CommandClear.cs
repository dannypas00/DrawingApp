using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace DrawingApp.CommandPattern
{
    internal class CommandClear : ICommand
    {
        private readonly CommandInvoker invoker;

        public CommandClear()
        {
            this.invoker = CommandInvoker.GetInstance();
        }

        public void Execute()
        {
            for (int i = invoker.MainWindow.canvas.Children.Count - 1; i > -1; i--)
            {
                var item = invoker.MainWindow.canvas.Children[i];
                if (item is Shape sItem) invoker.Map.Remove(sItem);
                invoker.MainWindow.canvas.Children.RemoveAt(i);
            }
            invoker.UpdateGroups();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
