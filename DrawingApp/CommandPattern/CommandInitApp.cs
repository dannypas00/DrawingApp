using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DrawingApp.CompositePattern;

namespace DrawingApp.CommandPattern
{
    internal class CommandInitApp : ICommand
    {
        private readonly CommandInvoker invoker;

        public CommandInitApp(CommandInvoker invoker)
        {
            this.invoker = invoker;
        }

        public void Execute()
        {
            //Logic to make sure the entire frontend is set up properly before calling it
            Group group = invoker.MainWindow.GetFile();
            invoker.MainWindow.groups.Items.Clear();
            ListBoxItem groupItem = new ListBoxItem {Content = @group.GetName(), IsEnabled = true};
            invoker.GroupMap.Add(groupItem, group);
            invoker.MainWindow.groups.Items.Add(groupItem);
            group.SetGroupItem(groupItem);
            invoker.MainWindow.groups.SelectedItem = groupItem;
            groupItem.IsSelected = true;
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }
    }
}