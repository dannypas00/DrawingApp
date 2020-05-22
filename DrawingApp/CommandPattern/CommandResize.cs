using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DrawingApp.CompositePattern;

namespace DrawingApp.CommandPattern
{
    internal class CommandResize : Command
    {
        private const double Multiplier = 0.005;
        private readonly CanvasShape shape;
        private int wheelDelta;

        public CommandResize(CanvasShape shape, MouseWheelEventArgs currMouseWheelEventArgs)
        {
            this.shape = shape;
            this.wheelDelta = -currMouseWheelEventArgs.Delta;
        }

        public void Execute()
        {
            double factor = wheelDelta;
            if (Math.Sign(factor) != -1)
            {
                shape.GetShape().Width *= factor * Multiplier; // * multiplier;
                shape.GetShape().Height *= factor * Multiplier; // * multiplier;
            }
            else
            {
                shape.GetShape().Width /= Math.Abs(factor) * Multiplier; // * multiplier;
                shape.GetShape().Height /= Math.Abs(factor) * Multiplier; // * multiplier;
            }
        }

        public void Redo()
        {
            Undo();
        }

        public void Undo()
        {
            wheelDelta *= -1;
            Execute();
        }
    }
}
