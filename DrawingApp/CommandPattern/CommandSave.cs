using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DrawingApp.CommandPattern
{
    class CommandSave : Command
    {
        private CommandInvoker invoker;

        public CommandSave(CommandInvoker invoker)
        {
            this.invoker = invoker;
        }

        public void Execute()
        {
            IGroupable[] shapes = invoker.GroupMap.Values.ToArray();
            List<string> lines = new List<string>();
            foreach (IGroupable item in shapes)
            {
                string postLine = "";
                string type = "";
                string indent = "";
                if (item is CanvasShape shape)
                {
                    double x = Canvas.GetLeft(shape.GetShape());
                    double y = Canvas.GetTop(shape.GetShape());
                    double h = shape.GetShape().Width;
                    double w = shape.GetShape().Height;
                    postLine = " " + x + " " + y + " " + h + " " + w;
                    if (shape.GetShape() is Rectangle || shape.GetShape() is Ellipse)
                    {
                        type = shape.GetName().Split(' ')[1];
                    }
                }
                //string type = item is Rectangle ? "rectangle" : "ellipse";
                if (item is Group tempItem)
                {
                    int tempCount = 0;
                    foreach (IGroupable tempChild in tempItem.GetChildren())
                    {
                        if (tempChild is CanvasShape)
                        {
                            tempCount++;
                        }
                    }
                    type = "group";
                    postLine = " " + tempCount;
                }
                for (int i = 0; i < item.GetDepth(); i++)
                {
                    indent += "    ";
                }

                string line = indent + type + postLine;
                lines.Add(line);
            }
            string pathWithEnv = @"%USERPROFILE%\Pictures\DrawingApp\save.txt";
            string filePath = Environment.ExpandEnvironmentVariables(pathWithEnv);
            System.IO.File.WriteAllLines(filePath, lines);
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
