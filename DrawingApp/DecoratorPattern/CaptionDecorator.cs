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
        private readonly Caption caption;

        public CaptionDecorator(DecoratorContext context)
        {
            this.context = context;
            this.caption = new Caption(context.parent.GetParent());
            context.MainWindow.canvas.Children.Add(caption.GetTextBox());
        }

        public void Draw()
        {
            caption.SetText("Yeet");
            caption.SetPosition(context.parent.GetPosition());
        }
    }
}
