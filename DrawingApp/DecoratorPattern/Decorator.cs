using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using DrawingApp.CompositePattern;

namespace DrawingApp.DecoratorPattern
{
    interface IDecorator
    {
        public void Draw();
    }
}
