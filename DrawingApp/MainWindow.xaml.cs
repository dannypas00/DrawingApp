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
        private SolidColorBrush colour = Brushes.Red;
        private int stroke;

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
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

            startPoint = e.GetPosition(canvas);
            shape.Fill = (bool)fill.IsChecked ? colour : Brushes.Transparent;
            shape.Stroke = colour;
            shape.StrokeThickness = stroke;
            Canvas.SetLeft(shape, startPoint.X);
            Canvas.SetTop(shape, startPoint.Y);
            canvas.Children.Add(shape);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
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

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            shape = null;
        }
    }
}
