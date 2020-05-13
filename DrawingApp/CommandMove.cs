using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace DrawingApp
{
    class CommandMove : Command
    {
        private Point origin = new Point(0, 0), offset = new Point(0, 0);
        private CanvasShape shape;
        private MainWindow mainWindow;
        private Point oldPos = new Point(0, 0);
        public CommandMove(CanvasShape shape, Point initialPos, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.shape = shape;
            this.oldPos.X = Canvas.GetLeft(shape.GetShape());
            this.oldPos.Y = Canvas.GetTop(shape.GetShape());
            offset.X = initialPos.X - Canvas.GetLeft(shape.GetShape());
            offset.Y = initialPos.Y - Canvas.GetTop(shape.GetShape());
        }

        public void Execute(MouseEventArgs e)
        {
            Point absolutePos = e.GetPosition(mainWindow.canvas);

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
