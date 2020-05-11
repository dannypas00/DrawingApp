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
        /*
        private Point startPoint;
        private string shapeName = "rectangle";
        private Shape shape;
        private Shape selected = null;
        private Point selectedPosition;
        private SolidColorBrush colour = Brushes.Red;
        private int stroke;
        private Point mouseOffset;
        */

        public CanvasShape selected = null;
        public string currentAction = "select";
        private CommandInvoker invoker;
        private bool mouseButtonHeld = false;
        private Point initialPosition;
        private Shape drawingShape;

        public MainWindow()
        {
            invoker = new CommandInvoker(this);
        }

        #region Drawing
        public void SetCanvasOffset(Point offset, Shape shape)
        {
            Canvas.SetLeft(shape, offset.X);
            Canvas.SetTop(shape, offset.Y);
            Trace.WriteLine("Setting new pos to " + offset.X + ", " + offset.Y);
        }

        /*
    private void Draw(object sender, MouseButtonEventArgs e)
    {
        try
        {
            stroke = Convert.ToInt16(strokeSize.Text);
        }
        catch (Exception)
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

    private void DrawMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Released || shape == null)
            return;

        Point pos = e.GetPosition(canvas);

        double x = Math.Min(pos.X, startPoint.X);
        double y = Math.Min(pos.Y, startPoint.Y);

        double w = Math.Max(pos.X, startPoint.X) - x;
        double h = Math.Max(pos.Y, startPoint.Y) - y;

        shape.Width = w;
        shape.Height = h;

        Canvas.SetLeft(shape, x);
        Canvas.SetTop(shape, y);
    }

    private void ResizeMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Released || shape == null)
            return;

        Point pos = e.GetPosition(canvas);
        Point selectedPos = new Point(Canvas.GetLeft(selected), Canvas.GetTop(selected));

        double x = Math.Min(pos.X, selectedPos.X);
        double y = Math.Min(pos.Y, selectedPos.Y);

        double w = Math.Max(pos.X, startPoint.X) - x;
        double h = Math.Max(pos.Y, startPoint.Y) - y;

        shape.Width = w;
        shape.Height = h;

        Canvas.SetLeft(shape, x);
        Canvas.SetTop(shape, y);
    }
    */
        #endregion

        #region Selection
        /*
    private void Select(object sender, MouseButtonEventArgs e)
    {
        Shape shape = (Shape)sender;
        if (selected == shape || actionBox.SelectedIndex == 0)
        {
            selected.StrokeDashArray = null;
            selected = null;
            return;
        }
        Trace.WriteLine("Selected something!");
        if (selected != null)
        {
            selected.StrokeDashArray = null;
        }
        selected = shape;
        mouseOffset = e.GetPosition(shape);
        shape.StrokeDashArray = new DoubleCollection() { 1 };

        double x = Canvas.GetLeft(shape);
        selectedPosition.X = x;

        double y = Canvas.GetTop(shape);
        selectedPosition.Y = y;
    }

    private void SelectMove(MouseEventArgs e)
    {
        double x = e.GetPosition(canvas).X;
        double y = e.GetPosition(canvas).Y;

        Canvas.SetLeft(selected, x - mouseOffset.X);
        Canvas.SetTop(selected, y - mouseOffset.Y);
    }
    */
        #endregion

        #region Button handling
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = true;
            initialPosition = e.GetPosition(canvas);
            switch (currentAction)
            {
                case "rectangle":
                    drawingShape = new Rectangle();
                    break;
                case "ellipse":
                    drawingShape = new Ellipse();
                    break;
                default:
                    break;
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //shape = null;
            mouseButtonHeld = false;
            if (currentAction == "rectangle" || currentAction == "ellipse")
            {
                //invoker.FinalizeDrawing();
                drawingShape = null;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                //selected.StrokeDashArray = null;
                //selected = null;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseButtonHeld == true)
            {
                switch (currentAction)    //TODO: Command pattern
                {
                    case "ellipse":
                    case "rectangle":
                        invoker.Draw(initialPosition, e.GetPosition(canvas), drawingShape);
                        break;
                    case "select":
                        invoker.Move(selected, e, initialPosition);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (selected != null)
            {
                invoker.Resize(selected, e);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            CommandInvoker.Save();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            CommandInvoker.Load();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            currentAction = "select";
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            currentAction = "ellipse";
            if (selected != null)
            {
                selected.Unselect();
                selected = null;
            }
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            currentAction = "rectangle";
            if (selected != null)
            {
                selected.Unselect();
                selected = null;
            }
        }
        #endregion
    }
}
