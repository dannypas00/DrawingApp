using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using DrawingApp.CompositePattern;

namespace DrawingApp.CommandPattern
{
    internal class CommandMove : Command
    {
        private Point origin = new Point(0, 0), offset = new Point(0, 0);
        private readonly CanvasShape shape;
        private readonly MainWindow mainWindow;
        private Point oldPos = new Point(0, 0);
        public MouseEventArgs CurrMouseEventArgs;

        public CommandMove(CanvasShape shape, Point initialPos, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.shape = shape;
            this.oldPos.X = Canvas.GetLeft(shape.GetShape());
            this.oldPos.Y = Canvas.GetTop(shape.GetShape());
            offset.X = initialPos.X - Canvas.GetLeft(shape.GetShape());
            offset.Y = initialPos.Y - Canvas.GetTop(shape.GetShape());
        }

        public void Execute()
        {
            Point absolutePos = CurrMouseEventArgs.GetPosition(mainWindow.canvas);

            double x = absolutePos.X - offset.X;
            double y = absolutePos.Y - offset.Y;

            mainWindow.SetCanvasOffset(new Point(x, y), shape.GetShape());
        }

        public void Redo()
        {
            Point newPos = oldPos;
            oldPos = new Point(Canvas.GetLeft(shape.GetShape()), Canvas.GetTop(shape.GetShape()));
            mainWindow.SetCanvasOffset(newPos, shape.GetShape());
        }

        public void Undo()
        {
            Point newPos = oldPos;
            oldPos = new Point(Canvas.GetLeft(shape.GetShape()), Canvas.GetTop(shape.GetShape()));
            mainWindow.SetCanvasOffset(newPos, shape.GetShape());
        }
    }
}
