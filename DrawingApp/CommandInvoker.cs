using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using DrawingApp.CommandPattern;

namespace DrawingApp
{
    public class CommandInvoker
    {
        private readonly Stack<Command> actionsDone = new Stack<Command>();
        private readonly Stack<Command> actionsUndone = new Stack<Command>();
        public Dictionary<ListBoxItem, IGroupable> groupMap = new Dictionary<ListBoxItem, IGroupable>();
        public MainWindow mainWindow;
        public Dictionary<Shape, CanvasShape> map = new Dictionary<Shape, CanvasShape>();

        public CommandInvoker(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

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
            //Command cmd;
            if (actionsUndone.TryPop(out var cmd))
            {
                cmd.Redo();
                actionsDone.Push(cmd);
            }
        }

        public void InitApp()
        {
            var cmd = new CommandInitApp();
            cmd.Execute(this);
        }

        public void Resize(CanvasShape shape, MouseWheelEventArgs e)
        {
            var cmd = new CommandResize();
            cmd.Execute(shape, e);
            actionsDone.Push(cmd);
        }

        public void Save()
        {
            var cmd = new CommandSave();
            cmd.Execute(map, groupMap);
        }

        public void Load()
        {
            var cmd = new CommandLoad();
            cmd.Execute(this);
        }

        public void Clear()
        {
            var cmd = new CommandClear(this);
            cmd.Execute();
            actionsDone.Push(cmd);
        }

        public void UpdateGroups()
        {
            var cmd = new CommandUpdateGroups();
            cmd.Execute(this);
        }

        public void AddGroup()
        {
            if (mainWindow.groups.SelectedItem != null)
            {
                var cmd = new CommandAddGroup();
                cmd.Execute((Group) groupMap[(ListBoxItem) mainWindow.groups.SelectedItem], this);
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
            Command cmd = new CommandDraw((int) Math.Round(x1), (int) Math.Round(y1), shape, this);
            actionsDone.Push(cmd);
        }

        /// <summary>
        /// Continue the drawing process started by StartDraw,
        /// it is run every cycle the mouse button is being pressed.
        /// </summary>
        /// <param name="x2">The x axis of the drawing's second point</param>
        /// <param name="y2">The y axis of the drawing's second point</param>
        public void Draw(double x2, double y2)
        {
            //Rounding positions to int to comply with mandatory saving grammar
            var cmd = (CommandDraw) actionsDone.Pop();
            cmd.Execute((int) Math.Round(x2), (int) Math.Round(y2));
            actionsDone.Push(cmd);
        }

        /// <summary>
        /// This starts the drawing process,
        /// it is run once when the mouse button is pressed,
        /// and has to be finished with the Draw function.
        /// </summary>
        /// <param name="p1">The drawing's first point</param>
        /// <param name="shape">The Shape object to draw with</param>
        public void StartDraw(Point p1, Shape shape)
        {
            StartDraw(p1.X, p1.Y, shape);
        }

        /// <summary>
        /// Continue the drawing process started by StartDraw,
        /// it is run every cycle the mouse button is being pressed.
        /// </summary>
        /// <param name="p2">The drawing's second point</param>
        public void Draw(Point p2)
        {
            Draw(p2.X, p2.Y);
        }
        #endregion

        #region Move
        //This region handles the two movement commands

        /// <summary>
        /// This starts the movement process,
        /// it is run once when the mouse button is pressed,
        /// and has to be finished with the Move function.
        /// </summary>
        /// <param name="shape">The shape to move</param>
        /// <param name="initialPos">The initial position of the mouse moving the shape</param>
        public void StartMove(CanvasShape shape, Point initialPos)
        {
            Command cmd = new CommandMove(shape, initialPos, mainWindow);
            actionsDone.Push(cmd);
        }

        /// <summary>
        /// Continue the movement process started by StartMove,
        /// it is run every cycle the mouse button is being pressed.
        /// </summary>
        /// <param name="e">The MouseEventArgs of the mouse moving the shape</param>
        public void Move(MouseEventArgs e)
        {
            var cmd = (CommandMove) actionsDone.Pop();
            cmd.Execute(e);
            actionsDone.Push(cmd);
        }
        #endregion
    }
}