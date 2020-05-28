using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using DrawingApp.CompositePattern;

namespace DrawingApp.CommandPattern
{
    internal class CommandLoad : ICommand
    {
        private readonly CommandInvoker invoker;

        public CommandLoad(CommandInvoker invoker)
        {
            this.invoker = invoker;
        }

        public void Execute()
        {
            //Set up variables
            //Set up the path to the save file TODO: Make this path selectable in runtime
            string pathWithEnv = @"%USERPROFILE%\Pictures\DrawingApp\save.txt";
            string filePath = Environment.ExpandEnvironmentVariables(pathWithEnv);
            string[] fileLines = File.ReadAllLines(filePath);
            int lineNr = 0;
            Dictionary<int, Group> lineGroupmap = new Dictionary<int, Group>();
            //Start off by clearing the old canvas
            invoker.Clear();
            invoker.MainWindow.groups.Items.Clear();
            invoker.MainWindow.GetFile().ClearChildren();
            invoker.GroupMap.Clear();
            invoker.Map.Clear();
            invoker.InitApp();
            invoker.MainWindow.HasUpdatedGroups = true;
            foreach (string line in fileLines)
            {
                if (lineNr == 0)
                {
                    lineGroupmap.Add(lineNr, (Group)invoker.GroupMap[(ListBoxItem)invoker.MainWindow.groups.SelectedItem]);
                    lineNr++;
                    continue;
                }
                string[] splitted = line.Split(' ');
                int depth = 0;
                for (int i = 0; i < splitted.Length; i++)
                {
                    string c = splitted[i];
                    if (c != "")
                    {
                        break;
                    }
                    else if (i % 4 == 0)
                    {
                        depth++;
                    }
                }

                //If the depth of the current line decreased compared to the last line
                if (invoker.GroupMap.Count > 0 && depth != invoker.GroupMap[(ListBoxItem)invoker.MainWindow.groups.SelectedItem].GetDepth() + 1)
                {
                    //Backtrack to find to what group the new line is supposed to belong
                    for (int i = lineNr - 1; i > -1; i--)
                    {
                        //Find first item with a lower depth
                        if (fileLines[i].Contains("group") && lineGroupmap[i].GetDepth() < depth)
                        {
                            invoker.MainWindow.groups.SelectedItem = lineGroupmap[i].GetGroupItem();
                            break;
                        }
                    }
                }

                //Process the first keyword after tabs
                switch (splitted[depth * 4])
                {
                    case "rectangle":
                        {   //Extra scope to prevent hiding x, y, w, and h
                            double x = Convert.ToInt32(splitted[1 + depth * 4]);
                            double y = Convert.ToInt32(splitted[2 + depth * 4]);
                            double w = Convert.ToInt32(splitted[3 + depth * 4]);
                            double h = Convert.ToInt32(splitted[4 + depth * 4]);
                            invoker.StartDraw(x, y, new Rectangle());
                            invoker.Draw(x + w, y + h);
                        }
                        break;
                    case "ellipse":
                        {   //Extra scope to prevent hiding x, y, w, and h
                            double x = Convert.ToInt32(splitted[1 + depth * 4]);
                            double y = Convert.ToInt32(splitted[2 + depth * 4]);
                            double w = Convert.ToInt32(splitted[3 + depth * 4]);
                            double h = Convert.ToInt32(splitted[4 + depth * 4]);
                            invoker.StartDraw(x, y, new Ellipse());
                            invoker.Draw(x + w, y + h);
                        }
                        break;
                    case "group":
                        invoker.AddGroup();
                        lineGroupmap.Add(lineNr, (Group)invoker.GroupMap[(ListBoxItem)invoker.MainWindow.groups.SelectedItem]);
                        break;
                    case "ornament":
                        Point relativeLocation = splitted[1 + depth * 4] switch
                        {
                            "top" => new Point(0, 1),
                            "bottom" => new Point(0, -1),
                            "left" => new Point(-1, 0),
                            "right" => new Point(1, 0),
                            _ => throw new InvalidDataException()
                        };
                        throw new NotImplementedException();
                        //break;
                    default:
                        continue;
                }
                lineNr++;
            }
            invoker.UpdateGroups();
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
