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
            Group group = invoker.mainWindow.file;
            invoker.mainWindow.groups.Items.Clear();
            ListBoxItem groupItem = new ListBoxItem();
            groupItem.Content = group.GetName();
            groupItem.IsEnabled = true;
            invoker.groupMap.Add(groupItem, group);
            invoker.mainWindow.groups.Items.Add(groupItem);
            group.SetGroupItem(groupItem);
            invoker.mainWindow.groups.SelectedItem = groupItem;
            groupItem.IsSelected = true;
        }
    }
}