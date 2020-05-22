using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DrawingApp.CommandPattern
{
    class CommandInitApp
    {
        public void Execute(CommandInvoker invoker)
        {
            Group group = invoker.MainWindow.GetFile();
            invoker.MainWindow.groups.Items.Clear();
            ListBoxItem groupItem = new ListBoxItem();
            groupItem.Content = group.GetName();
            groupItem.IsEnabled = true;
            invoker.GroupMap.Add(groupItem, group);
            invoker.MainWindow.groups.Items.Add(groupItem);
            group.SetGroupItem(groupItem);
            invoker.MainWindow.groups.SelectedItem = groupItem;
            groupItem.IsSelected = true;
        }
    }
}