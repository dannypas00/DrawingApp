using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DrawingApp.CompositePattern;

namespace DrawingApp.CommandPattern
{
    //Refactor to just adding item to existing group, no need to update all groups every time
    internal class CommandUpdateGroups : Command
    {
        private readonly CommandInvoker invoker;

        public CommandUpdateGroups(CommandInvoker invoker)
        {
            this.invoker = invoker;
        }

        /// <summary>
        /// Updates the group sidebar to contain any newly drawn shapes.
        /// </summary>
        public void Execute()
        {
            //Define the group tree structure's root and traverse it with a recursive search algorithm
            Group group = invoker.MainWindow.GetFile();
            foreach (IGroupable g in TraverseGroup(group))
            {
                //Runs for every group that isn't already included in the GroupMap
                if (invoker.GroupMap.ContainsValue(g)) continue;
                ListBoxItem groupItem = new ListBoxItem
                {
                    Content = g.GetName().Split(' ')[0],
                    IsEnabled = g is Group,
                    Margin = new Thickness(5 * g.GetDepth(), 0, 0, 0)
                };
                //Add the new object and ListboxItem to the GroupMap dictionary and the sidebar list
                invoker.GroupMap.Add(groupItem, g);
                invoker.MainWindow.groups.Items.Add(groupItem);
                g.SetGroupItem(groupItem);
            }
            //Sort the sidebar Listbox by name, thus creating the correct tree structure
            invoker.MainWindow.groups.Items.SortDescriptions.Clear();
            invoker.MainWindow.groups.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Content", System.ComponentModel.ListSortDirection.Ascending));
            foreach (ListBoxItem item in invoker.MainWindow.groups.Items)
            {
                item.Content = invoker.GroupMap[item].GetName();
            }
        }

        /// <summary>
        /// Recursively searches the group tree structure, taking item as the root.
        /// </summary>
        /// <param name="item">Tree root from which to search.</param>
        /// <returns>An unsorted list of all IGroupable items in the group tree structure</returns>
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
