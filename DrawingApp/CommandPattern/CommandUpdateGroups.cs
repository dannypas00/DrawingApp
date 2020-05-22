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
    internal class CommandUpdateGroups : Command
    {
        public void Execute(CommandInvoker invoker)
        {
            Group group = invoker.MainWindow.GetFile();
            foreach (IGroupable g in TraverseGroup(group))
            {
                //Runs for every other group
                if (invoker.GroupMap.ContainsValue(g)) continue;
                ListBoxItem groupItem = new ListBoxItem
                {
                    Content = g.GetName().Split(' ')[0],
                    IsEnabled = g is Group,
                    Margin = new Thickness(5 * g.GetDepth(), 0, 0, 0)
                };
                invoker.GroupMap.Add(groupItem, g);
                invoker.MainWindow.groups.Items.Add(groupItem);
                g.SetGroupItem(groupItem);
            }
            invoker.MainWindow.groups.Items.SortDescriptions.Clear();
            invoker.MainWindow.groups.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Content", System.ComponentModel.ListSortDirection.Ascending));
            foreach (ListBoxItem item in invoker.MainWindow.groups.Items)
            {
                item.Content = invoker.GroupMap[item].GetName();
            }
        }

        private static IEnumerable<IGroupable> TraverseGroup(IGroupable item)
        {
            List<IGroupable> result = new List<IGroupable>();
            if (item == null)
            {
                return null;
            }
            result.Add(item);
            if (item is Group g)
            {
                foreach (IGroupable i in g.GetChildren())
                {
                    result.AddRange(TraverseGroup(i));
                }
            }
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
