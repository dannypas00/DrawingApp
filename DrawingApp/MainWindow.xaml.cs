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
        public CanvasShape selected = null;
        public string currentAction = "select";
        private CommandInvoker invoker;
        private bool mouseButtonHeld = false;
        private Point initialPosition;

        public MainWindow()
        {
            invoker = new CommandInvoker(this);
        }

        public void SetCanvasOffset(Point offset, Shape shape)
        {
            Canvas.SetLeft(shape, offset.X);
            Canvas.SetTop(shape, offset.Y);
            Trace.WriteLine("Setting new pos to " + offset.X + ", " + offset.Y);
        }

        #region Button handling
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = true;
            initialPosition = e.GetPosition(canvas);
            switch (currentAction)
            {
                case "rectangle":
                    invoker.StartDraw(initialPosition.X, initialPosition.Y, new Rectangle());
                    break;
                case "ellipse":
                    invoker.StartDraw(initialPosition.X, initialPosition.Y, new Ellipse());
                    break;
                default:
                    break;
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseButtonHeld == true)
            {
                switch (currentAction)
                {
                    case "ellipse":
                    case "rectangle":
                        invoker.Draw(e.GetPosition(canvas));
                        break;
                    case "select":
                        if (selected != null)
                        {
                            invoker.Move(selected, e, initialPosition);
                        }
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
            invoker.Save();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Load();
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
