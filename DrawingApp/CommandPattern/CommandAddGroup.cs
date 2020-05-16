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
            invoker.UpdateGroups(child);
            child.GetGroupItem().IsSelected = true;
            invoker.mainWindow.groups.SelectedItem = child.GetGroupItem();
        }

        public void Redo()
        {
            parent.AddChild(child);
            invoker.UpdateGroups(child);
        }

        public void Undo()
        {
            parent.RemoveChild(child);
            invoker.mainWindow.groups.Items.Remove(invoker.groupMap[child]);
        }
    }
}
