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
using DrawingApp.DecoratorPattern;

namespace DrawingApp.CommandPattern
{
    internal class CommandDraw : ICommand
    {
        private readonly int x1, y1;
        public int X2, Y2;
        private readonly CommandInvoker invoker;
        private readonly Shape shape;
        private readonly CanvasShape canvShape;
        private readonly IDecorator capDecorator;

        public CommandDraw(int x1, int y1, Shape shape)
        {
            //Set starting location for drawing the shape
            this.x1 = x1;
            this.y1 = y1;
            this.invoker = CommandInvoker.GetInstance();
            this.shape = shape;
            //Get the group that is selected in the group sidebar
            Group selected = invoker.MainWindow.groups.SelectedItem != null ? (Group)invoker.GroupMap[(ListBoxItem)invoker.MainWindow.groups.SelectedItem] : (Group)invoker.MainWindow.groups.Items[0];
            //Make a new CanvasShape for calling functions on the new shape
            canvShape = new CanvasShape(shape, selected);
            //Setup the parent-child relationship of the new shape
            Group parent = selected;
            parent.AddChild(canvShape);
            //Map the CanvasShape that owns the Shape to it for easy correlation
            invoker.Map.Add(shape, canvShape);
            //New event handler for selecting the shape
            shape.MouseDown += Select;
            //Setup visuals
            shape.Stroke = shape.Fill = CommandInvoker.RandomColor();
            shape.StrokeThickness = 3;
            capDecorator = new CaptionDecorator(new DecoratorContext(canvShape.GetPosition(), "bottom", canvShape));
            canvShape.decorator = capDecorator;
            invoker.MainWindow.canvas.Children.Add(shape);
        }

        public void Execute()
        {
            //Set ending location for drawing the shapes
            int x = (int)Math.Min(x1, X2);    //Om **Maxime's** bug te voorkomen
            int y = (int)Math.Min(y1, Y2);    //Om **Maxime's** bug te voorkomen

            int w = (int)Math.Max(x1, X2) - x;//Om **Maxime's** bug te voorkomen
            int h = (int)Math.Max(y1, Y2) - y;//Om **Maxime's** bug te voorkomen

            //Move shape into its correct place
            System.Drawing.Point pos = new System.Drawing.Point(x, y);
            invoker.MainWindow.SetCanvasOffset(pos, shape);
            canvShape.SetPosition(pos);
            shape.Width = w;
            shape.Height = h;
            capDecorator.Draw();

            //Update group structure to represent added shape
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
            if (!(sender is Shape) || invoker.MainWindow.CurrentAction != "select") return;
            CanvasShape parent = invoker.Map[shape];
            if (invoker.MainWindow.Selected != null)
            {
                invoker.MainWindow.Selected.Unselect();
                invoker.MainWindow.Selected = null;
            }
            if (invoker.MainWindow.Selected == parent) return;
            invoker.MainWindow.Selected = parent;
            invoker.StartMove(parent, e.GetPosition(invoker.MainWindow.canvas));
            parent.Select();
        }
    }
}
