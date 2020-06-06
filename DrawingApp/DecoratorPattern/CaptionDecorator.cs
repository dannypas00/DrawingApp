using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Windows.Controls;
using DrawingApp.CompositePattern;

namespace DrawingApp.DecoratorPattern
{
    class CaptionDecorator : IDecorator
    {
        private readonly DecoratorContext context;

        public CaptionDecorator(DecoratorContext context)
        {
            this.context = context;
        }

        public void Draw()
        {
            Caption caption = new Caption(context.parent.GetParent());
            Canvas.SetTop(caption.GetTextBox(), context.parent.GetPosition().Y);
            Canvas.SetLeft(caption.GetTextBox(), context.parent.GetPosition().X);
            //Find way to change position on screen
            caption.SetText("Yeet");
            caption.SetPosition(context.parent.GetPosition());
            Trace.WriteLine("Drew " + caption.GetTextBox().Text);
            context.MainWindow.canvas.Children.Add(caption.GetTextBox());
        }
    }
}
