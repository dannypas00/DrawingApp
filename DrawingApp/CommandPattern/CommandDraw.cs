using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using DrawingApp.CompositePattern;

namespace DrawingApp.CommandPattern
{
    internal class CommandDraw : Command
    {
        private readonly int x1, y1;
        public int X2, Y2;
        private readonly CommandInvoker invoker;
        private readonly Shape shape;
        private readonly CanvasShape canvShape;
        private static Random _rnd = new Random();

        public CommandDraw(int x1, int y1, Shape shape, CommandInvoker invoker)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.invoker = invoker;
            this.shape = shape;

            Group selected = invoker.MainWindow.groups.SelectedItem != null ? (Group)invoker.GroupMap[(ListBoxItem)invoker.MainWindow.groups.SelectedItem] : (Group)invoker.MainWindow.groups.Items[0];
            canvShape = new CanvasShape(shape, selected);
            Group parent = (Group)invoker.GroupMap[(ListBoxItem)invoker.MainWindow.groups.SelectedItem];
            parent.AddChild(canvShape);
            invoker.Map.Add(shape, canvShape);
            shape.MouseDown += new MouseButtonEventHandler(Select);
            shape.Stroke = shape.Fill = CommandInvoker.RandomColor();
            shape.StrokeThickness = 3;
            invoker.MainWindow.canvas.Children.Add(shape);
            Trace.WriteLine(invoker.MainWindow.groups.SelectedItem.ToString());
        }

        public void Execute()
        {
            int x = (int)Math.Min(x1, X2);    //Om **Maxime's** bug te voorkomen
            int y = (int)Math.Min(y1, Y2);    //Om **Maxime's** bug te voorkomen

            int w = (int)Math.Max(x1, X2) - x;//Om **Maxime's** bug te voorkomen
            int h = (int)Math.Max(y1, Y2) - y;//Om **Maxime's** bug te voorkomen

            invoker.MainWindow.SetCanvasOffset(new Point(x, y), shape);
            shape.Width = w;
            shape.Height = h;

            invoker.UpdateGroups();
        }

        public void Redo()
        {
            invoker.Map.Add(shape, canvShape);
            invoker.MainWindow.canvas.Children.Add(shape);
            //BUG: listbox of groups doesn't update properly when redoing a draw
        }

        public void Undo()
        {
            invoker.Map.Remove(shape);
            invoker.MainWindow.canvas.Children.Remove(shape);
            //BUG: listbox of groups doesn't update properly when undoing a draw
        }

        private void Select(object sender, MouseButtonEventArgs e)
        {
            if (sender is Shape && invoker.MainWindow.CurrentAction == "select")
            {
                Shape shape = (Shape)sender;
                CanvasShape parent = invoker.Map[shape];
                if (invoker.MainWindow.Selected != null)
                {
                    invoker.MainWindow.Selected.Unselect();
                    invoker.MainWindow.Selected = null;
                }
                if (invoker.MainWindow.Selected != parent)
                {
                    invoker.MainWindow.Selected = parent;
                    invoker.StartMove(parent, e.GetPosition(invoker.MainWindow.canvas));
                    parent.Select();
                }
            }
        }
    }
}
