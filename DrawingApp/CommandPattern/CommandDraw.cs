using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawingApp.CommandPattern
{
    class CommandDraw : Command
    {
        private double x1, y1, x2, y2;
        private CommandInvoker invoker;
        private Shape shape;
        private CanvasShape canvShape;

        public CommandDraw(int x1, int y1, Shape shape, CommandInvoker invoker)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.invoker = invoker;
            this.shape = shape;

            Group selected = invoker.mainWindow.groups.SelectedItem != null ? (Group)invoker.groupMap[(ListBoxItem)invoker.mainWindow.groups.SelectedItem] : (Group)invoker.mainWindow.groups.Items[0];
            canvShape = new CanvasShape(shape, selected);
            Group parent = (Group)invoker.groupMap[(ListBoxItem)invoker.mainWindow.groups.SelectedItem];
            parent.AddChild(canvShape);
            invoker.map.Add(shape, canvShape);
            shape.MouseDown += new MouseButtonEventHandler(Select);
            shape.Fill = Brushes.Red;
            shape.Stroke = Brushes.Red;
            shape.StrokeThickness = 3;
            invoker.mainWindow.canvas.Children.Add(shape);
            Trace.WriteLine(invoker.mainWindow.groups.SelectedItem.ToString());
        }

        public void Execute(double x2, double y2)
        {
            int x = (int)Math.Round(Math.Min(x1, x2));    //Om **Maxime's** bug te voorkomen
            int y = (int)Math.Round(Math.Min(y1, y2));    //Om **Maxime's** bug te voorkomen

            int w = (int)Math.Round(Math.Max(x1, x2) - x);//Om **Maxime's** bug te voorkomen
            int h = (int)Math.Round(Math.Max(y1, y2) - y);//Om **Maxime's** bug te voorkomen

            invoker.mainWindow.SetCanvasOffset(new Point(x, y), shape);
            shape.Width = w;
            shape.Height = h;

            this.x2 = x2;
            this.y2 = y2;
            invoker.UpdateGroups();
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
            if (sender is Shape && invoker.mainWindow.CurrentAction == "select")
            {
                Shape shape = (Shape)sender;
                CanvasShape parent = invoker.map[shape];
                if (invoker.mainWindow.Selected != null)
                {
                    invoker.mainWindow.Selected.Unselect();
                    invoker.mainWindow.Selected = null;
                }
                if (invoker.mainWindow.Selected != parent)
                {
                    invoker.mainWindow.Selected = parent;
                    invoker.StartMove(parent, e.GetPosition(invoker.mainWindow.canvas));
                    parent.Select();
                }
            }
        }
    }
}
