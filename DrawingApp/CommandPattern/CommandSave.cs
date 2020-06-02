using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using DrawingApp.CompositePattern;
using DrawingApp.VisitorPattern;

namespace DrawingApp.CommandPattern
{
    internal class CommandSave : ICommand
    {
        private readonly CommandInvoker invoker;

        public CommandSave()
        {
            this.invoker = CommandInvoker.GetInstance();
        }

        public void Execute()
        {

            string pathWithEnv = @"%USERPROFILE%\Pictures\DrawingApp\save.txt";
            string filePath = Environment.ExpandEnvironmentVariables(pathWithEnv);
            invoker.GroupMap.Values.ToArray()[0].Accept(new SaveVisitor(filePath, (Group) invoker.GroupMap.Values.ToArray()[0]));
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
