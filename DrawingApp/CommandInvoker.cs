﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using DrawingApp.CommandPattern;

namespace DrawingApp
{
    public class CommandInvoker
    {
        private Stack<Command> actionsDone = new Stack<Command>(), actionsUndone = new Stack<Command>();
        public MainWindow mainWindow;
        public Dictionary<Shape, CanvasShape> map = new Dictionary<Shape, CanvasShape>();
        public Dictionary<ListBoxItem, IGroupable> groupMap = new Dictionary<ListBoxItem, IGroupable>();

        public CommandInvoker(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
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
            if (actionsUndone.TryPop(out Command cmd))
            {
                cmd.Redo();
                actionsDone.Push(cmd);
            }
        }

        public void InitApp()
        {
            CommandInitApp cmd = new CommandInitApp();
            cmd.Execute(this);
        }

        #region Drawing
        public void StartDraw(double x1, double y1, Shape shape)
        {
            //Rounding positions to int to comply with mandatory saving grammar
            Command cmd = new CommandDraw((int)Math.Round(x1), (int)Math.Round(y1), shape, this);
            actionsDone.Push(cmd);
        }

        public void Draw(double x2, double y2)
        {
            //Rounding positions to int to comply with mandatory saving grammar
            CommandDraw cmd = (CommandDraw)actionsDone.Pop();
            cmd.Execute((int)Math.Round(x2), (int)Math.Round(y2));
            actionsDone.Push(cmd);
        }

        public void StartDraw(Point p2, Shape shape)
        {
            StartDraw(p2.X, p2.Y, shape);
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

        public void Save()
        {
            CommandSave cmd = new CommandSave();
            cmd.Execute(map, groupMap);
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

        public void UpdateGroups()
        {
            CommandUpdateGroups cmd = new CommandUpdateGroups();
            cmd.Execute(this);
        }

        public void AddGroup()
        {
            if (mainWindow.groups.SelectedItem != null)
            {
                CommandAddGroup cmd = new CommandAddGroup();
                cmd.Execute((Group)groupMap[(ListBoxItem)mainWindow.groups.SelectedItem], this);
                actionsDone.Push(cmd);
            }
        }
    }
}
