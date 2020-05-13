using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DrawingApp
{
    class CommandResize : Command
    {
        private double multiplier = 0.005;

        public CommandResize()
        {

        }

        public void Execute(CanvasShape shape, MouseWheelEventArgs e)
        {
            double factor = -e.Delta;
            if (Math.Sign(factor) != -1)
            {
                shape.GetShape().Width *= factor * multiplier; // * multiplier;
                shape.GetShape().Height *= factor * multiplier; // * multiplier;
            }
            else
            {
                shape.GetShape().Width /= Math.Abs(factor) * multiplier; // * multiplier;
                shape.GetShape().Height /= Math.Abs(factor) * multiplier; // * multiplier;
            }
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
