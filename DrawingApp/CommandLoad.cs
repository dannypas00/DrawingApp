using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Shapes;

namespace DrawingApp
{
    class CommandLoad : Command
    {
        public CommandLoad()
        {

        }

        public void Execute(CommandInvoker invoker)
        {
            invoker.Clear();
            string line;
            var pathWithEnv = @"%USERPROFILE%\Pictures\DrawingApp\save.txt";
            var filePath = Environment.ExpandEnvironmentVariables(pathWithEnv);
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                string[] splitted = line.Split(' ');
                Shape shape;
                switch (splitted[0])
                {
                    case "rectangle":
                        shape = new Rectangle();
                        break;
                    case "ellipse":
                        shape = new Ellipse();
                        break;
                    default:
                        continue;
                }
                double x = Convert.ToDouble(splitted[1]);
                double y = Convert.ToDouble(splitted[2]);
                double w = Convert.ToDouble(splitted[3]);
                double h = Convert.ToDouble(splitted[4]);
                invoker.StartDraw(x, y, shape);
                invoker.Draw(x + w, y + h);
            }

            file.Close();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
