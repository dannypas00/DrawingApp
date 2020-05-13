using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
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
        private int depth = 0;

        public CanvasShape(Shape shape)
        {
            this.shape = shape;
            if (shape is System.Windows.Shapes.Rectangle)
            {
                name = "rectangle";
            }
            else if (shape is Ellipse)
            {
                name = "ellipse";
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
