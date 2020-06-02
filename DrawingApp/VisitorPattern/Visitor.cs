using System;
using System.Collections.Generic;
using System.Text;
using DrawingApp.CompositePattern;

namespace DrawingApp.VisitorPattern
{
    public interface IVisitor
    {
        public void VisitCanvasShape(CanvasShape e);
        public void VisitGroup(Group e);
    }
}
