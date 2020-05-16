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
        public Group file;
        private bool mouseButtonHeld = false;
        private Point initialPosition;
        private bool hasUpdatedGroups = false;

        public MainWindow()
        {
            invoker = new CommandInvoker(this);
            file = new Group();
        }

        public void SetCanvasOffset(Point offset, Shape shape)
        {
            Canvas.SetLeft(shape, offset.X);
            Canvas.SetTop(shape, offset.Y);
        }

        #region Button handling
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!hasUpdatedGroups)
            {
                invoker.UpdateGroups();
                hasUpdatedGroups = true;
            }
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
        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Undo();
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Redo();
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

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Clear();
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

        private void AddGroup_Click(object sender, RoutedEventArgs e)
        {
            invoker.AddGroup();
        }
        #endregion
    }
}
