using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DrawingApp.CommandPattern
{
    class CommandLoad : Command
    {
        public void Execute(CommandInvoker invoker)
        {
            invoker.Clear();
            string pathWithEnv = @"%USERPROFILE%\Pictures\DrawingApp\save.txt";
            string filePath = Environment.ExpandEnvironmentVariables(pathWithEnv);
            invoker.MainWindow.groups.Items.Clear();
            invoker.MainWindow.GetFile().ClearChildren();
            invoker.GroupMap.Clear();
            invoker.Map.Clear();
            int lineNr = 0;
            Dictionary<int, Group> lineGroupmap = new Dictionary<int, Group>();
            invoker.InitApp();
            invoker.MainWindow.HasUpdatedGroups = true;
            string[] fileLines = File.ReadAllLines(filePath);
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

                if (invoker.GroupMap.Count > 0 && depth != invoker.GroupMap[(ListBoxItem)invoker.MainWindow.groups.SelectedItem].GetDepth() + 1)
                {
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

                switch (splitted[depth * 4])
                {
                    case "rectangle":
                        {   //Extra scope om xywh opnieuw te kunnen gebruiken voor code cleanness
                            double x = Convert.ToInt32(splitted[1 + depth * 4]);
                            double y = Convert.ToInt32(splitted[2 + depth * 4]);
                            double w = Convert.ToInt32(splitted[3 + depth * 4]);
                            double h = Convert.ToInt32(splitted[4 + depth * 4]);
                            invoker.StartDraw(x, y, new Rectangle());
                            invoker.Draw(x + w, y + h);
                        }
                        break;
                    case "ellipse":
                        {
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
                        Point relativeLocation;
                        switch (splitted[1 + depth * 4])
                        {
                            case "top":
                                relativeLocation = new Point(0, 1);
                                break;
                            case "bottom":
                                relativeLocation = new Point(0, -1);
                                break;
                            case "left":
                                relativeLocation = new Point(-1, 0);
                                break;
                            case "right":
                                relativeLocation = new Point(1, 0);
                                break;
                            default:
                                throw new InvalidDataException();
                        }
                        throw new NotImplementedException();
                        break;
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
