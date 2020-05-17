using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace DrawingApp
{
    public class Group : IGroupable
    {
        private List<IGroupable> children = new List<IGroupable>();
        private Group parent;
        private string name = "";
        private int brothers = 0;
        private int depth = 0;
        private ListBoxItem groupItem;

        public Group(Group parent = null)
        {
            this.parent = parent;
            if (parent != null)
            {
                foreach (IGroupable g in parent.GetChildren()) {
                    if (g is Group)
                    {
                        brothers++;
                    }
                }
                this.name = parent.GetName() + "." + (brothers + 1);
            }
            else
            {
                this.name = "1";
            }
            Group previewParent = parent;
            while (previewParent != null)
            {
                depth++;
                previewParent = previewParent.GetParent();
            }
        }

        public void AddChild(IGroupable child)
        {
            children.Add(child);
        }

        public List<IGroupable> GetChildren()
        {
            return children;
        }

        public void RemoveChild(IGroupable child)
        {
            children.Remove(child);
        }

        public void RemoveChildAt(int index)
        {
            children.RemoveAt(index);
        }

        public Group GetParent()
        {
            return parent;
        }

        public void SetParent(Group parent)
        {
            this.parent = parent;
        }

        public ListBoxItem GetGroupItem()
        {
            return groupItem;
        }

        public void SetGroupItem(ListBoxItem groupItem)
        {
            this.groupItem = groupItem;
        }

        public string GetName()
        {
            return name;
        }

        public int GetDepth()
        {
            return depth;
        }
    }
}
