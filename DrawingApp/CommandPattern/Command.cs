using System;
using System.Collections.Generic;
using System.Text;

namespace DrawingApp.CommandPattern
{
    interface Command
    {
        public void Undo();
        public void Redo();
        public void Execute();
    }
}
