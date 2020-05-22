using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DrawingApp.CommandPattern
{
    class CommandAddGroup : Command
    {
        Group parent;
        Group child;
        CommandInvoker invoker;

        public void Execute(Group parent, CommandInvoker invoker)
        {
            this.parent = parent;
            this.invoker = invoker;
            child = new Group(parent);
            parent.AddChild(child);
            invoker.UpdateGroups();
            child.GetGroupItem().IsSelected = true;
            invoker.MainWindow.groups.SelectedItem = child.GetGroupItem();
        }

        public void Redo()
        {
            parent.AddChild(child);
            invoker.UpdateGroups();
        }

        public void Undo()
        {
            parent.RemoveChild(child);
            invoker.MainWindow.groups.Items.Remove(child.GetGroupItem());
        }
    }
}
