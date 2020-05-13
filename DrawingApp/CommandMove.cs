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
        public CommandMove(CanvasShape shape, Point initialPos, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.shape = shape;
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
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
