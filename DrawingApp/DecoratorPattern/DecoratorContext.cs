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
        public string CaptionPosition;  //"top" "bottom" "left" "right"
        public CanvasShape shape;
        public MainWindow MainWindow = CommandInvoker.GetInstance().MainWindow;
        private readonly string[] positions = {"top", "bottom", "left", "right"};

        public DecoratorContext(System.Drawing.Point ShapePosition = new System.Drawing.Point(), string CaptionPosition = "",
            CanvasShape shape = null)
        {

            this.CaptionPosition = CaptionPosition;
            this.shape = shape;
            this.CaptionPosition = positions[CommandInvoker.Rnd.Next(0, 4)];
        }
    }
}
