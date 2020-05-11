using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Shapes;

namespace DrawingApp
{
    public class CanvasShape
    {
        private bool selected = false;
        private Point position1 = new Point(), position2 = new Point();
        private Shape shape;

        public CanvasShape(Shape shape)
        {
            this.shape = shape;
        }

        public void Unselect()
        {
            selected = false;
        }

        public CanvasShape Select()
        {
            selected = true;
            return this;
        }
    }
}
