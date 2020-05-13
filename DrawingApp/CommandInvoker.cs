using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawingApp
{
    public class CommandInvoker
    {
        private Stack<Command> actionsDone = new Stack<Command>(), actionsUndone = new Stack<Command>();
        public MainWindow mainWindow;
        public Dictionary<Shape, CanvasShape> map = new Dictionary<Shape, CanvasShape>();

        public CommandInvoker(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        #region Drawing
        public void StartDraw(double x1, double y1, Shape shape)
        {
            Command cmd = new CommandDraw(x1, y1, shape, this);
            actionsDone.Push(cmd);
        }

        public void Draw(double x2, double y2)
        {
            CommandDraw cmd = (CommandDraw)actionsDone.Pop();
            cmd.Execute(x2, y2);
            actionsDone.Push(cmd);
        }

        public void Draw(Point p2)
        {
            Draw(p2.X, p2.Y);
        }
        #endregion

        #region Move
        public void StartMove(CanvasShape shape, Point initialPos)
        {
            Command cmd = new CommandMove(shape, initialPos, mainWindow);
            actionsDone.Push(cmd);
        }

        public void Move(CanvasShape shape, MouseEventArgs e, Point initialPos)
        {
            CommandMove cmd = (CommandMove)actionsDone.Pop();
            cmd.Execute(e);
            actionsDone.Push(cmd);
        }
        #endregion

        public void Resize(CanvasShape shape, MouseWheelEventArgs e)
        {
            CommandResize cmd = new CommandResize();
            cmd.Execute(shape, e);
            actionsDone.Push(cmd);
        }

        public void Undo()
        {
            //Undo top action on actionsDone stack
            //Push undone action to actionsUndone stack
            if (actionsDone.TryPop(out Command cmd))
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
            if (actionsUndone.TryPop(out Command cmd)) {
                cmd.Redo();
                actionsDone.Push(cmd);
            }
        }

        public void Save()
        {
            CommandSave cmd = new CommandSave();
            cmd.Execute(map);
        }

        public void Load()
        {
            CommandLoad cmd = new CommandLoad();
            cmd.Execute(this);
        }

        public void Clear()
        {
            CommandClear cmd = new CommandClear(this);
            cmd.Execute();
            actionsDone.Push(cmd);
        }
    }
}
