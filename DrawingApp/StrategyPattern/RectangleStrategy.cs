using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;

namespace DrawingApp.StrategyPattern
{
    class RectangleStrategy : IStrategy
    {
        public void ExecuteStrategy()
        {
            CommandInvoker.GetInstance().StartDraw(CommandInvoker.GetInstance().MainWindow.InitialPosition.X, CommandInvoker.GetInstance().MainWindow.InitialPosition.Y, new Rectangle());
        }
    }
}
