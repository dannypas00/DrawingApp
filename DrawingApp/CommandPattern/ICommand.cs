using System;
using System.Collections.Generic;
using System.Text;

namespace DrawingApp.CommandPattern
{
    interface ICommand
    {
        /// <summary>
        /// Undo the last undoable action
        /// </summary>
        public void Undo();

        /// <summary>
        /// Redo the last undone action
        /// </summary>
        public void Redo();

        /// <summary>
        /// Execute the given command
        /// </summary>
        public void Execute();
    }
}
