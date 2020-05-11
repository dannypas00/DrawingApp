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
        //private List<KeyValuePair<Shape, CanvasShape>> map = new List<KeyValuePair<Shape, CanvasShape>>();
        private Dictionary<Shape, CanvasShape> map = new Dictionary<Shape, CanvasShape>();
        private SolidColorBrush color = Brushes.Red;
        private float stroke = 3;
        private Canvas canvas;

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

        public void FinalizeDrawing()
        {
            
        }
        #endregion

        public void Move(CanvasShape shape)
        {
            throw new NotImplementedException();
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
            if (sender is Shape)
            {
                Shape shape = (Shape)sender;
                CanvasShape parent = map[shape];
                if (mainWindow.selected != null)
                {
                    mainWindow.selected.Unselect();
                }
                if (sender is Shape)
                {
                    parent.Select();
                }
            }
        }
    }
}
