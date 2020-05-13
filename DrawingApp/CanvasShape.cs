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

        public CanvasShape(Shape shape)
        {
            this.shape = shape;
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
    }
}
