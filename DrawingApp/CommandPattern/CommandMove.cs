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
    internal class CommandMove : ICommand
    {
        private Point offset = new Point(0, 0);
        private readonly CanvasShape shape;
        private readonly MainWindow mainWindow;
        private System.Drawing.Point oldPos = new System.Drawing.Point(0, 0);
        public MouseEventArgs CurrMouseEventArgs;

        public CommandMove(CanvasShape shape, Point initialPos, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.shape = shape;
            oldPos.X = (int) MathF.Round((float) Canvas.GetLeft(shape.GetShape()));
            oldPos.Y = (int) MathF.Round((float) Canvas.GetTop(shape.GetShape()));
            offset.X = initialPos.X - Canvas.GetLeft(shape.GetShape());
            offset.Y = initialPos.Y - Canvas.GetTop(shape.GetShape());
        }

        public void Execute()
        {
            Point absolutePos = CurrMouseEventArgs.GetPosition(mainWindow.canvas);

            int x = Convert.ToInt32(absolutePos.X - offset.X);
            int y = Convert.ToInt32(absolutePos.Y - offset.Y);
            System.Drawing.Point newPoint = new System.Drawing.Point(x, y);

            mainWindow.SetCanvasOffset(newPoint, shape.GetShape());
            shape.decorator.Draw();
        }

        public void Redo()
        {
            System.Drawing.Point newPos = oldPos;
            oldPos = new System.Drawing.Point((int)MathF.Round((float) Canvas.GetLeft(shape.GetShape())), (int)MathF.Round((float) Canvas.GetTop(shape.GetShape())));
            mainWindow.SetCanvasOffset(newPos, shape.GetShape());
        }

        public void Undo()
        {
            System.Drawing.Point newPos = oldPos;
            oldPos = new System.Drawing.Point((int)MathF.Round((float) Canvas.GetLeft(shape.GetShape())), (int)MathF.Round((float) Canvas.GetTop(shape.GetShape())));
            mainWindow.SetCanvasOffset(newPos, shape.GetShape());
        }
    }
}
