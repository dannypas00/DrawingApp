using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using DrawingApp.CompositePattern;

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
        private Point initialPosition;
        private readonly CommandInvoker invoker;
        private bool mouseButtonHeld;
        public CanvasShape Selected;

        public MainWindow()
        {
            file = new Group();
            invoker = new CommandInvoker(this);
        }

        #region Mouse button handling
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!HasUpdatedGroups)
            {
                invoker.InitApp();
                HasUpdatedGroups = true;
            }

            foreach (Button b in new List<Button>() { ClearButton, EllipseButton, SaveButton, AddGroup, RectangleButton, LoadButton, UndoButton, RedoButton, ClearButton })
            {
                if (b != null)
                    b.Background = CommandInvoker.RandomColor();
            }

            mouseButtonHeld = true;
            initialPosition = e.GetPosition(canvas);
            switch (CurrentAction)
            {
                case "rectangle":
                    invoker.StartDraw(initialPosition.X, initialPosition.Y, new Rectangle());
                    break;
                case "ellipse":
                    invoker.StartDraw(initialPosition.X, initialPosition.Y, new Ellipse());
                    break;
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseButtonHeld)
            {
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

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentAction = "ellipse";
            if (Selected == null) return;
            Selected.Unselect();
            Selected = null;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Clear();
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentAction = "rectangle";
            if (Selected == null) return;
            Selected.Unselect();
            Selected = null;
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