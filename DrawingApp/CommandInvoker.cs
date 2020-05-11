using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawingApp
{
    public class CommandInvoker
    {
        private Stack<String> actionsDone, actionsUndone;
        private MainWindow mainWindow;
        private Dictionary<Shape, CanvasShape> map = new Dictionary<Shape, CanvasShape>();
        private SolidColorBrush color = Brushes.Red;
        private float stroke = 3;
        private double mouseOffsetX = -999, mouseOffsetY = -999;

        public CommandInvoker(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        #region Drawing
        public void Draw(double x1, double y1, double x2, double y2, Shape shape)
        {
            if (!map.ContainsKey(shape))
            {
                CanvasShape canvShape = new CanvasShape(shape);
                map.Add(shape, canvShape);
                shape.MouseDown += new MouseButtonEventHandler(Select);
                shape.Fill = color;
                shape.Stroke = color;
                shape.StrokeThickness = stroke;
                mainWindow.canvas.Children.Add(shape);
            }
            double x = Math.Min(x1, x2);    //Om **Maxime's** bug te voorkomen
            double y = Math.Min(y1, y2);    //Om **Maxime's** bug te voorkomen
            
            double w = Math.Max(x1, x2) - x;//Om **Maxime's** bug te voorkomen
            double h = Math.Max(y1, y2) - y;//Om **Maxime's** bug te voorkomen
            mainWindow.SetCanvasOffset(new Point(x, y), shape);
            shape.Width = w;
            shape.Height = h;
        }

        public void Draw(Point p1, Point p2, Shape shape)
        {
            Draw(p1.X, p1.Y, p2.X, p2.Y, shape);
        }
        #endregion

        public void Move(CanvasShape shape, MouseEventArgs e, Point initialPos)
        {
            Point relativePos = e.GetPosition(shape.GetShape());
            Point absolutePos = e.GetPosition(mainWindow.canvas);

            if (mouseOffsetX == -999 || mouseOffsetY == -999)
            {
                mouseOffsetX = initialPos.X - Canvas.GetLeft(shape.GetShape());
                mouseOffsetY = initialPos.Y - Canvas.GetTop(shape.GetShape());
            }

            double x = absolutePos.X - mouseOffsetX;
            double y = absolutePos.Y - mouseOffsetY;

            mainWindow.SetCanvasOffset(new Point(x, y), shape.GetShape());
        }

        public void Undo()
        {
            //Undo top action on actionsDone stack
            //Push undone action to actionsUndone stack
            throw new NotImplementedException();
        }

        public void Redo()
        {
            //Redo top action on actionsUndone stack
            //Push redone action to actionsDone stack
            throw new NotImplementedException();
        }

        public void Resize(CanvasShape shape, MouseWheelEventArgs e)
        {
            double factor = -e.Delta;
            //factor = Math.Sign(factor) == -1 ? 1 / Math.Abs(factor) : factor;
            double multiplier = 0.005;
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

        internal static void Save()
        {
            throw new NotImplementedException();
        }

        internal static void Load()
        {
            throw new NotImplementedException();
        }

        private void Select(object sender, MouseButtonEventArgs e)
        {
            if (sender is Shape && mainWindow.currentAction == "select")
            {
                Shape shape = (Shape)sender;
                CanvasShape parent = map[shape];
                if (mainWindow.selected != null)
                {
                    mainWindow.selected.Unselect();
                    mainWindow.selected = null;
                }
                if (mainWindow.selected != parent)
                {
                    mainWindow.selected = parent;
                    parent.Select();
                }
            }
        }
    }
}
