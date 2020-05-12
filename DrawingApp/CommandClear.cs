using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace DrawingApp
{
    class CommandClear : Command
    {
        private CommandInvoker invoker;
        private UIElementCollection removed;

        public CommandClear(CommandInvoker invoker)
        {
            this.invoker = invoker;
        }

        public void Execute()
        {
            removed = invoker.mainWindow.canvas.Children;
            for (int i = 0; i < invoker.mainWindow.canvas.Children.Count; i++)
            {
                invoker.mainWindow.canvas.Children.RemoveAt(i);
            }
            foreach (Shape shape in invoker.map.Keys)
            {
                invoker.map.Remove(shape);
            }
        }

        public void Undo()
        {
            foreach(UIElement element in removed) 
            {
                invoker.mainWindow.canvas.Children.Add(element);
            }
        }
    }
}
