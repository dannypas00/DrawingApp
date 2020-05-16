using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DrawingApp.CommandPattern
{
    //Refactor to just adding item to existing group, no need to update all groups every time
    class CommandUpdateGroups : Command
    {
        public void Execute(CommandInvoker invoker)
        {
            Group group = invoker.mainWindow.file;
            invoker.mainWindow.groups.Items.Clear();
            if (invoker.mainWindow.groups.SelectedItem == null)
            {
                ListBoxItem groupItem = new ListBoxItem();
                groupItem.Content = group.GetName();
                groupItem.IsEnabled = group is Group;
                groupItem.Margin = new Thickness(10 * group.GetDepth(), 0, 0, 0);
                invoker.groupMap.Add(groupItem, group);
                invoker.mainWindow.groups.Items.Add(groupItem);
                group.SetGroupItem(groupItem);
                invoker.mainWindow.groups.SelectedItem = groupItem;
            }
            foreach (IGroupable g in TraverseGroup(group))
            {
                //if (!invoker.groupMap.ContainsValue(g))
                //{
                    ListBoxItem groupItem = new ListBoxItem();
                    groupItem.Content = g.GetName();
                    groupItem.IsEnabled = g is Group;
                    groupItem.Margin = new Thickness(10 * g.GetDepth(), 0, 0, 0);
                    invoker.groupMap.Add(groupItem, g);
                    invoker.mainWindow.groups.Items.Add(groupItem);
                    g.SetGroupItem(groupItem);
                    if (g is Group)
                    {
                        invoker.mainWindow.groups.SelectedItem = groupItem;
                    }
                //}
            }
        }

        private List<IGroupable> TraverseGroup(IGroupable item)
        {
            List<IGroupable> result = new List<IGroupable>();
            if (item == null)
                return null;
            result.Add(item);
            if (item is Group)
            {
                Group g = (Group)item;
                foreach (IGroupable i in g.GetChildren())
                {
                    result.AddRange(TraverseGroup(i));
                }
            }
            Trace.WriteLine(item.GetName());
            return result;
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
