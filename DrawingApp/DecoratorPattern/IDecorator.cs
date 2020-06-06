using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using DrawingApp.CompositePattern;

namespace DrawingApp.DecoratorPattern
{
    public interface IDecorator
    {
        public void Draw();
    }
}
