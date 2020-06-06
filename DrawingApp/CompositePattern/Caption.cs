using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using DrawingApp.VisitorPattern;

namespace DrawingApp.CompositePattern
{
    public class Caption : IGroupable
    {
        private readonly List<IGroupable> children = new List<IGroupable>();
        private Group parent;
        private readonly string name = "";
        private readonly int depth = 0;
        private ListBoxItem groupItem;
        private readonly TextBox text = new TextBox();
        private Point position;

        public Caption(Group parent)
        {
            this.parent = parent;
        }

        public void SetText(string text)
        {
            this.text.Text = text;
        }

        public TextBox GetTextBox()
        {
            return this.text;
        }

        public void SetPosition(Point position)
        {
            this.position = position;
            Canvas.SetLeft(text, position.X);
            Canvas.SetTop(text, position.Y);
        }

        public Point GetPosition()
        {
            return this.position;
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

        public void ClearChildren()
        {
            children.Clear();
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

        public void Accept(IVisitor v)
        {
            v.VisitCaption(this);
        }
    }
}
