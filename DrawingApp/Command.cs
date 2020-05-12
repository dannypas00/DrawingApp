using System;
using System.Collections.Generic;
using System.Text;

namespace DrawingApp
{
    interface Command
    {
        public void Undo();
    }
}
