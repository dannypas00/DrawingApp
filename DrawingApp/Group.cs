using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingApp
{
    public class Group : IGroupable
    {
        private List<IGroupable> children = new List<IGroupable>();
        private Group parent;
        private string name = "";
        private int brothers = 0;

        public Group(Group parent = null)
        {
            this.parent = parent;
            if (parent != null)
            {
                foreach (Group g in parent.GetChildren()) {
                    brothers++;
                }
                this.name = parent.GetName() + "." + (brothers + 1);
            }
            else
            {
                this.name = "Group 1";
            }
        }

        public void AddChild(IGroupable child)
        {
            children.Add(child);
        }

        public void RemoveChild(IGroupable child)
        {
            children.Remove(child);
        }

        public void RemoveChildAt(int index)
        {
            children.RemoveAt(index);
        }

        public List<IGroupable> GetChildren()
        {
            return children;
        }

        public Group GetParent()
        {
            return parent;
        }

        public string GetName()
        {
            return name;
        }
    }
}
