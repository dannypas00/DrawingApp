using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point startPoint;
        private string shapeName = "rectangle";
        private Shape shape;
        private Shape selected = null;
        private SolidColorBrush colour = Brushes.Red;
        private int stroke;
        private Point mouseOffset;

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (actionBox.SelectedIndex)    //TODO: Command pattern
            {
                case 0:
                    Draw(sender, e);
                    break;
                default: 
                    break;
            }
        }

        private void Draw(object sender, MouseButtonEventArgs e)
        {
            try
            {
                stroke = Convert.ToInt16(strokeSize.Text);
            }
            catch (Exception ex)
            {
                stroke = 3;
            }

            shapeName = shapeBox.SelectedIndex.ToString();

            switch (shapeName)
            {
                case "0":
                    shape = new Rectangle();
                    break;
                case "1":
                    shape = new Ellipse();
                    Trace.WriteLine("Drawing ellipse!");
                    break;
                default:
                    shape = new Rectangle();
                    break;
            }

            shape.MouseDown += new MouseButtonEventHandler(Select);
            startPoint = e.GetPosition(canvas);
            shape.Fill = (bool)fill.IsChecked ? colour : Brushes.Transparent;
            shape.Stroke = colour;
            shape.StrokeThickness = stroke;
            Canvas.SetLeft(shape, startPoint.X);
            Canvas.SetTop(shape, startPoint.Y);
            canvas.Children.Add(shape);
        }

        private void Select(object sender, MouseButtonEventArgs e)
        {
            Trace.WriteLine("Selected something!");
            selected = (Shape) sender;
            mouseOffset = e.GetPosition((Shape) sender);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            switch (actionBox.SelectedIndex)    //TODO: Command pattern
            {
                case 0:
                    DrawMove(sender, e);
                    break;
                case 1:
                    if (e.LeftButton == MouseButtonState.Pressed && selected != null)
                    {
                        SelectMove(e);
                    }
                    break;
            }
        }

        private void DrawMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || shape == null)
                return;

            var pos = e.GetPosition(canvas);

            var x = Math.Min(pos.X, startPoint.X);
            var y = Math.Min(pos.Y, startPoint.Y);

            var w = Math.Max(pos.X, startPoint.X) - x;
            var h = Math.Max(pos.Y, startPoint.Y) - y;

            shape.Width = w;
            shape.Height = h;

            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
        }
        private void SelectMove(MouseEventArgs e)
        {
            Canvas.SetLeft(selected, e.GetPosition(canvas).X - mouseOffset.X);
            Canvas.SetTop(selected, e.GetPosition(canvas).Y - mouseOffset.Y);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            shape = null;
            selected = null;
        }
    }
}
