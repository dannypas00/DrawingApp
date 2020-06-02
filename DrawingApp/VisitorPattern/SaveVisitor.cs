using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using DrawingApp.CompositePattern;

namespace DrawingApp.VisitorPattern
{
    class SaveVisitor : IVisitor
    {
        private readonly List<string> buffer = new List<string>();
        private readonly string filePath;
        private readonly Group root;

        public SaveVisitor(string filePath, Group root)
        {
            this.root = root;
            this.filePath = filePath;
            //System.IO.File.WriteAllLines(filePath, buffer);
        }

        public void VisitCanvasShape(CanvasShape e)
        {
            string postLine;
            string type = e.GetName().Split(' ')[1]; ;
            string indent = "";
            double x = Canvas.GetLeft(e.GetShape());
            double y = Canvas.GetTop(e.GetShape());
            double h = e.GetShape().Width;
            double w = e.GetShape().Height;
            postLine = " " + x + " " + y + " " + h + " " + w;
            for (int i = 0; i < e.GetDepth(); i++)
            {
                indent += "    ";
            }

            string line = indent + type + postLine;
            buffer.Add(line);
        }

        public void VisitGroup(Group e)
        {
            string postLine;
            string type = "group";
            string indent = "";
            int tempCount = 0;

            foreach (IGroupable tempChild in e.GetChildren())
            {
                if (tempChild is CanvasShape)
                {
                    tempCount++;
                }
            }
            postLine = " " + tempCount;

            for (int i = 0; i < e.GetDepth(); i++)
            {
                indent += "    ";
            }

            string line = indent + type + postLine;
            buffer.Add(line);

            foreach (IGroupable g in e.GetChildren())
            {
                g.Accept(this);
            }

            if (e == root)
            {
                System.IO.File.WriteAllLines(filePath, buffer);
            }
        }
    }
}
