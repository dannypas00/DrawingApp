using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using DrawingApp.CompositePattern;
using DrawingApp.StrategyPattern;

namespace DrawingApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string CurrentAction = "select";
        private readonly Group file;
        public bool HasUpdatedGroups;
        public Point InitialPosition;
        private readonly CommandInvoker invoker;
        private bool mouseButtonHeld;
        private IStrategy strategy;
        public CanvasShape Selected;

        public MainWindow()
        {
            file = new Group();
            invoker = CommandInvoker.GetInstance();
        }

        #region Mouse button handling
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!HasUpdatedGroups)
            {
                invoker.MainWindow = this;
                invoker.InitApp();
                HasUpdatedGroups = true;
            }

            /*foreach (var b in new List<Button>() { ClearButton, ellipse, SaveButton, AddGroup, rectangle, LoadButton, UndoButton, RedoButton, ClearButton }.Where(b => b != null))
            {
                b.Background = CommandInvoker.RandomColor();
            }*/

            mouseButtonHeld = true;
            InitialPosition = e.GetPosition(canvas);
            if (CurrentAction != "rectangle" && CurrentAction != "ellipse") return;
            strategy.ExecuteStrategy();
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseButtonHeld) return;
            switch (CurrentAction)
            {
                case "ellipse":
                case "rectangle":
                    invoker.Draw(e.GetPosition(canvas));
                    break;
                case "select":
                    if (Selected != null) invoker.Move(e);
                    break;
            }
        }

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Selected != null) invoker.Resize(Selected, e);
        }
        #endregion

        #region Toolbar Button handling
        //All functions in this region are called upon pressing their respective buttons 
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
            CurrentAction = "select";
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Undo();
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Redo();
        }

        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button) sender;
            CurrentAction = s.Name;
            if (CurrentAction == "rectangle")
            {
                strategy = new RectangleStrategy();
            }
            else if (CurrentAction == "ellipse")
            {
                strategy = new EllipseStrategy();
            }
            Trace.WriteLine("Name: " + CurrentAction);
            if (Selected == null) return;
            Selected.Unselect();
            Selected = null;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Clear();
        }

        private void AddGroup_Click(object sender, RoutedEventArgs e)
        {
            invoker.AddGroup();
        }
        #endregion

        /// <summary>
        /// Changes the position of a Shape in
        /// relation to the canvas.
        /// </summary>
        /// <param name="offset">New offset for the shape</param>
        /// <param name="shape">Shape to reposition</param>
        public void SetCanvasOffset(Point offset, Shape shape)
        {
            Canvas.SetLeft(shape, offset.X);
            Canvas.SetTop(shape, offset.Y);
        }

        public Group GetFile()
        {
            return file;
        }
    }
}