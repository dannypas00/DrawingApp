using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using DrawingApp.CommandPattern;
using DrawingApp.CompositePattern;
using ICommand = DrawingApp.CommandPattern.ICommand;

namespace DrawingApp
{
    public class CommandInvoker
    {
        private readonly Stack<ICommand> actionsDone = new Stack<ICommand>();
        private readonly Stack<ICommand> actionsUndone = new Stack<ICommand>();
        private static readonly CommandInvoker Instance = new CommandInvoker();
        public Dictionary<ListBoxItem, IGroupable> GroupMap = new Dictionary<ListBoxItem, IGroupable>();
        public MainWindow MainWindow;
        public Dictionary<Shape, CanvasShape> Map = new Dictionary<Shape, CanvasShape>();
        public static readonly Random Rnd = new Random();

        private CommandInvoker() {}

        public void Undo()
        {
            //Undo top action on actionsDone stack
            //Push undone action to actionsUndone stack
            if (actionsDone.TryPop(out var cmd))
            {
                cmd.Undo();
                actionsUndone.Push(cmd);
            }
        }

        public void Redo()
        {
            //Redo top action on actionsUndone stack
            //Push redone action to actionsDone stack
            if (actionsUndone.TryPop(out var cmd))
            {
                cmd.Redo();
                actionsDone.Push(cmd);
            }
        }

        public void InitApp()
        {
            var cmd = new CommandInitApp();
            cmd.Execute();
        }

        public void Resize(CanvasShape shape, MouseWheelEventArgs e)
        {
            var cmd = new CommandResize(shape, e);
            cmd.Execute();
            actionsDone.Push(cmd);
        }

        public void Save()
        {
            var cmd = new CommandSave();
            cmd.Execute();
        }

        public void Load()
        {
            var cmd = new CommandLoad();
            cmd.Execute();
        }

        public void Clear()
        {
            var cmd = new CommandClear();
            cmd.Execute();
            actionsDone.Push(cmd);
        }

        public void UpdateGroups()
        {
            ICommand cmd = new CommandUpdateGroups();
            cmd.Execute();
        }

        public void AddGroup()
        {
            if (MainWindow.groups.SelectedItem != null)
            {
                var cmd = new CommandAddGroup((Group) GroupMap[(ListBoxItem) MainWindow.groups.SelectedItem]);
                cmd.Execute();
                actionsDone.Push(cmd);
            }
        }

        #region Drawing
        //This region handles all the drawing commands called on the invoker

        /// <summary>
        /// Start the drawing process,
        /// it is run once when the mouse button is pressed,
        /// and has to be finished with the Draw function.
        /// </summary>
        /// <param name="x1">The x axis of the drawing's first point</param>
        /// <param name="y1">The y axis of the drawing's first point</param>
        /// <param name="shape">The Shape object to draw with</param>
        public void StartDraw(double x1, double y1, Shape shape)
        {
            //Rounding positions to int to comply with mandatory saving grammar
            ICommand cmd = new CommandDraw((int) Math.Round(x1), (int) Math.Round(y1), shape);
            actionsDone.Push(cmd);
        }

        /// <summary>
        /// Continues the drawing process started by StartDraw,
        /// it is intended to run every cycle the mouse button is being pressed.
        /// </summary>
        /// <param name="x2">The x axis of the drawing's second point</param>
        /// <param name="y2">The y axis of the drawing's second point</param>
        public void Draw(double x2, double y2)
        {
            //Rounding positions to int to comply with mandatory saving grammar
            var cmd = (CommandDraw) actionsDone.Pop();
            cmd.X2 = (int) Math.Round(x2);
            cmd.Y2 = (int) Math.Round(y2);
            cmd.Execute();
            actionsDone.Push(cmd);
        }

        /// <summary>
        /// Starts the drawing process,
        /// it is intended to run once when the mouse button is pressed,
        /// and to be finished with the Draw function.
        /// </summary>
        /// <param name="p1">The drawing's first point</param>
        /// <param name="shape">The Shape object to draw with</param>
        public void StartDraw(Point p1, Shape shape)
        {
            StartDraw(p1.X, p1.Y, shape);
        }

        /// <summary>
        /// Continues the drawing process started by StartDraw,
        /// it is intended to run every cycle the mouse button is being pressed.
        /// </summary>
        /// <param name="p2">The drawing's second point</param>
        public void Draw(Point p2)
        {
            Draw(p2.X, p2.Y);
        }
        #endregion

        #region Movement
        //This region handles the two movement commands

        /// <summary>
        /// Starts the movement process,
        /// it is intended to run once when the mouse button is pressed,
        /// and to be finished with the Move function.
        /// </summary>
        /// <param name="shape">The shape to move</param>
        /// <param name="initialPos">The initial position of the mouse moving the shape</param>
        public void StartMove(CanvasShape shape, Point initialPos)
        {
            ICommand cmd = new CommandMove(shape, initialPos, MainWindow);
            actionsDone.Push(cmd);
        }

        /// <summary>
        /// Continues the movement process started by StartMove,
        /// it is intended to run every cycle the mouse button is being pressed.
        /// </summary>
        /// <param name="e">The MouseEventArgs of the mouse moving the shape</param>
        public void Move(MouseEventArgs e)
        {
            var cmd = (CommandMove) actionsDone.Pop();
            cmd.CurrMouseEventArgs = e;
            cmd.Execute();
            actionsDone.Push(cmd);
        }
        #endregion

        /// <summary>
        /// Generates a randomly colored brush
        /// </summary>
        /// <returns>A SolidColorBrush with the random color</returns>
        public static SolidColorBrush RandomColor()
        {
            var hex = $"#{Rnd.Next(0x1000000):X6}";
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        }

        public static CommandInvoker GetInstance()
        {
            return Instance;
        }
    }
}