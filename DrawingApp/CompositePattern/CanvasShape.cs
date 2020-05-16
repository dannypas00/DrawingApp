using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DrawingApp
{
    public class CanvasShape : IGroupable
    {
        private bool selected = false;
        private Point position1 = new Point(), position2 = new Point();
        private Shape shape;
        private Group parent;
        private string name = "";
        private int depth = 1;
        private ListBoxItem groupItem;

        public CanvasShape(Shape shape, Group parent)
        {
            this.shape = shape;
            this.parent = parent;
            if (shape is System.Windows.Shapes.Rectangle)
            {
                name = parent.GetGroupItem().Content.ToString().Split(' ')[0] + " rectangle";
            }
            else if (shape is Ellipse)
            {
                name = parent.GetGroupItem().Content.ToString().Split(' ')[0] + " ellipse";
            }
            Group previewParent = parent;
            while (previewParent != null)
            {
                depth++;
                previewParent = previewParent.GetParent();
            }
        }

        public void Unselect()
        {
            selected = false;
            shape.StrokeDashArray = null;
        }

        public CanvasShape Select()
        {
            selected = true;
            shape.StrokeDashArray = new System.Windows.Media.DoubleCollection() { 1 };
            return this;
        }

        public Shape GetShape()
        {
            return shape;
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
