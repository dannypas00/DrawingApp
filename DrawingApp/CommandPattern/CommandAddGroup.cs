using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DrawingApp.CommandPattern
{
    class CommandAddGroup : Command
    {
        public Group Parent, Child;
        public CommandInvoker Invoker;

        public CommandAddGroup(Group parent, CommandInvoker invoker)
        {
            this.Parent = parent;
            this.Invoker = invoker;
        }

        public void Execute()
        {
            Child = new Group(Parent);
            Parent.AddChild(Child);
            Invoker.UpdateGroups();
            Child.GetGroupItem().IsSelected = true;
            Invoker.MainWindow.groups.SelectedItem = Child.GetGroupItem();
        }

        public void Redo()
        {
            Parent.AddChild(Child);
            Invoker.UpdateGroups();
        }

        public void Undo()
        {
            Parent.RemoveChild(Child);
            Invoker.MainWindow.groups.Items.Remove(Child.GetGroupItem());
        }
    }
}
