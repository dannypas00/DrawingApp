using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DrawingApp.CompositePattern;
using Point = System.Drawing.Point;

namespace DrawingApp.DecoratorPattern
{
    class CaptionDecorator : IDecorator
    {
        private readonly DecoratorContext context;
        private readonly Caption caption;
        private System.Drawing.Point offset;
        public IDecorator parent;

        public CaptionDecorator(DecoratorContext context, IDecorator parent = null)
        {
            this.context = context;
            this.caption = new Caption(context.shape.GetParent());
            caption.SetText("Yeet");
            this.parent = parent;
            context.MainWindow.canvas.Children.Add(caption.GetTextBox());
        }

        public void Draw()
        {
            if (caption.GetTextBox() != null || caption.GetTextBox().Width > 0 ||
                caption.GetTextBox().Height > 0)
            {
                switch (context.CaptionPosition)
                {
                    case "top":
                        offset = new Point(0, (int) MathF.Round((float) -20));
                        break;
                    case "bottom":
                        offset = new Point(0, (int) MathF.Round((float) context.shape.GetShape().Height));
                        break;
                    case "left":
                        offset = new Point(-8 * caption.GetTextBox().Text.Length, 0);
                        caption.GetTextBox().HorizontalContentAlignment = HorizontalAlignment.Right;
                        break;
                    case "right":
                        offset = new Point((int) MathF.Round((float) context.shape.GetShape().Width), 0);
                        break;
                    default:
                        offset = new Point(0, 0);
                        break;
                }
            }

            //caption.SetText("Yeet");
            caption.SetPosition(new Point(context.shape.GetPosition().X + offset.X, context.shape.GetPosition().Y + offset.Y));
        }
    }
}
