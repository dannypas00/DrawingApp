using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using DrawingApp.CompositePattern;

namespace DrawingApp.DecoratorPattern
{
    public class DecoratorContext
    {
        public Point ShapePosition;
        public Point DecoratorPosition;
        public string CaptionPosition;  //"top" "bottom" "left" "right"
        public CanvasShape parent;
        public MainWindow MainWindow = CommandInvoker.GetInstance().MainWindow;

        public DecoratorContext(Point ShapePosition = new Point(), string CaptionPosition = "",
            CanvasShape parent = null)
        {
            this.ShapePosition = ShapePosition;
            this.CaptionPosition = CaptionPosition;
            this.parent = parent;
            this.CaptionPosition = "bottom";
        }
    }
}
