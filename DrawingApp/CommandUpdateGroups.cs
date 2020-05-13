using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DrawingApp
{
    class CommandUpdateGroups : Command
    {
        public void Execute(MainWindow mainWindow)
        {
            Group group = mainWindow.file;
            //Trace.WriteLine(TraverseGroup(group));
            //TraverseGroup(group);
            mainWindow.groups.Items.Clear();
            foreach (IGroupable g in TraverseGroup(group))
            {
                ListBoxItem groupItem = new ListBoxItem();
                groupItem.Content = g.GetName();
                groupItem.Margin = new Thickness(10 * g.GetDepth(), 0, 0, 0);
                if (g == group)
                {
                    groupItem.IsSelected = true;
                    mainWindow.groups.SelectedItem = groupItem;
                }
                mainWindow.groups.Items.Add(groupItem);
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
