using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DrawingApp
{
    class CommandSave : Command
    {
        public CommandSave()
        {
            //In een lijn van de array moet het volgende staan: shape left top width height
            //Shape wordt tijdelijk opgeslagen tijdens het runnen, dus shape opvragen
            
        }

        public void Execute(Dictionary<Shape, CanvasShape> map)
        {
            Shape[] shapes = map.Keys.ToArray();
            List<string> lines = new List<string>();
            foreach (Shape shape in shapes)
            {
                double x = Canvas.GetLeft(shape);
                double y = Canvas.GetTop(shape);
                double h = shape.Width;
                double w = shape.Height;
                string type = shape is Rectangle ? "rectangle" : "ellipse";

                string line = type + " " + x + " " + y + " " + h + " " + w;
                lines.Add(line);
                Trace.WriteLine(line);
                //Save naar @"%userprofile%\Pictures\DrawingApp\save.txt
            }
            var pathWithEnv = @"%USERPROFILE%\Pictures\DrawingApp\save.txt";
            var filePath = Environment.ExpandEnvironmentVariables(pathWithEnv);
            System.IO.File.WriteAllLines(filePath, lines);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
