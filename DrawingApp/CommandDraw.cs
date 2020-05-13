using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawingApp
{
    class CommandDraw : Command
    {
        private double x1, y1, x2, y2;
        private CommandInvoker invoker;
        private Shape shape;
        private CanvasShape canvShape;

        public CommandDraw(double x1, double y1, Shape shape, CommandInvoker invoker)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.invoker = invoker;
            this.shape = shape;
            
            canvShape = new CanvasShape(shape);
            invoker.map.Add(shape, canvShape);
            shape.MouseDown += new MouseButtonEventHandler(Select);
            shape.Fill = Brushes.Red;
            shape.Stroke = Brushes.Red;
            shape.StrokeThickness = 3;
            invoker.mainWindow.canvas.Children.Add(shape);
        }

        public void Execute(double x2, double y2)
        {
            double x = Math.Min(x1, x2);    //Om **Maxime's** bug te voorkomen
            double y = Math.Min(y1, y2);    //Om **Maxime's** bug te voorkomen

            double w = Math.Max(x1, x2) - x;//Om **Maxime's** bug te voorkomen
            double h = Math.Max(y1, y2) - y;//Om **Maxime's** bug te voorkomen

            invoker.mainWindow.SetCanvasOffset(new Point(x, y), shape);
            shape.Width = w;
            shape.Height = h;

            this.x2 = x2;
            this.y2 = y2;
        }

        public void Redo()
        {
            invoker.map.Add(shape, canvShape);
            invoker.mainWindow.canvas.Children.Add(shape);
        }

        public void Undo()
        {
            invoker.map.Remove(shape);
            invoker.mainWindow.canvas.Children.Remove(shape);
        }

        private void Select(object sender, MouseButtonEventArgs e)
        {
            if (sender is Shape && invoker.mainWindow.currentAction == "select")
            {
                Shape shape = (Shape)sender;
                CanvasShape parent = invoker.map[shape];
                if (invoker.mainWindow.selected != null)
                {
                    invoker.mainWindow.selected.Unselect();
                    invoker.mainWindow.selected = null;
                }
                if (invoker.mainWindow.selected != parent)
                {
                    invoker.mainWindow.selected = parent;
                    invoker.StartMove(parent, e.GetPosition(invoker.mainWindow.canvas));
                    parent.Select();
                }
            }
        }
    }
}
