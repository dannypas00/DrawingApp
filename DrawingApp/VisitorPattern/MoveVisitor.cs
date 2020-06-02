using System;
using System.Collections.Generic;
using System.Text;
using DrawingApp.CompositePattern;

namespace DrawingApp.VisitorPattern
{
    class MoveVisitor : IVisitor
    {
        public void VisitCanvasShape(CanvasShape e)
        {
            throw new NotImplementedException();
        }

        public void VisitGroup(Group e)
        {
            throw new NotImplementedException();
        }
    }
}
