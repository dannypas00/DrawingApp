﻿using System;
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
            for (int i = invoker.mainWindow.canvas.Children.Count - 1; i > 0; i--)
            {
                invoker.map.Remove((Shape)invoker.mainWindow.canvas.Children[i]);
                invoker.mainWindow.canvas.Children.RemoveAt(i);
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
