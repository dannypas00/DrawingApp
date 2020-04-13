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
        private Rectangle selectBox1 = new Rectangle
            {
                Width = 10,
                Height = 10,
                Stroke = Brushes.Gray,
                Fill = Brushes.Gray,
                StrokeThickness = 4
            };

        private new Rectangle selectBox2 = new Rectangle
            {
                Width = 10,
                Height = 10,
                Stroke = Brushes.Gray,
                Fill = Brushes.Gray,
                StrokeThickness = 4
            };

        private Point selectedPosition;
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
            Shape shape = (Shape)sender;
            if (selected == shape) return;
            Trace.WriteLine("Selected something!");
            if (selected != null)
            {
                selected.StrokeDashArray = null;
/*                canvas.Children.Remove(selectBox1);
                canvas.Children.Remove(selectBox2);*/
            }
            else
            {
                canvas.Children.Add(selectBox1);
                canvas.Children.Add(selectBox2);
            }
            selected = shape;
            mouseOffset = e.GetPosition(shape);
            shape.StrokeDashArray = new DoubleCollection() { 1 };

            double x = Canvas.GetLeft(shape);
            selectedPosition.X = x;

            double y = Canvas.GetTop(shape);
            selectedPosition.Y = y;

            Canvas.SetLeft(selectBox1, x - (selectBox1.Width / 2));
            Canvas.SetTop(selectBox1, y - (selectBox1.Height / 2));

            Canvas.SetLeft(selectBox2, x + shape.Width - (selectBox2.Width / 2));
            Canvas.SetTop(selectBox2, y + shape.Height - (selectBox2.Height / 2));
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
            double x = e.GetPosition(canvas).X;
            double y = e.GetPosition(canvas).Y;

            Canvas.SetLeft(selected, x - mouseOffset.X);
            Canvas.SetTop(selected, y - mouseOffset.Y);

            Canvas.SetLeft(selectBox1, x - mouseOffset.X - (selectBox1.Width / 2));
            Canvas.SetTop(selectBox1, y - mouseOffset.Y - (selectBox1.Height / 2));

            Canvas.SetLeft(selectBox2, x + selected.Width - mouseOffset.X - (selectBox1.Width / 2));
            Canvas.SetTop(selectBox2, y + selected.Height - mouseOffset.Y - (selectBox1.Height / 2));
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            shape = null;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                selected.StrokeDashArray = null;
                selected = null;
            }
        }
    }
}
