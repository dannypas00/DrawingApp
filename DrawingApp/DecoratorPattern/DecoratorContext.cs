using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using DrawingApp.CompositePattern;
using static System.Drawing.Point;

namespace DrawingApp.DecoratorPattern
{
    public class DecoratorContext
    {
        public System.Drawing.Point ShapePosition;
        public System.Drawing.Point DecoratorPosition;
        public string CaptionPosition;  //"top" "bottom" "left" "right"
        public CanvasShape parent;
        public MainWindow MainWindow = CommandInvoker.GetInstance().MainWindow;

        public DecoratorContext(System.Drawing.Point ShapePosition = new System.Drawing.Point(), string CaptionPosition = "",
            CanvasShape parent = null)
        {
            this.ShapePosition = ShapePosition;
            this.CaptionPosition = CaptionPosition;
            this.parent = parent;
            this.CaptionPosition = "bottom";
        }
    }
}
