using System;
using System.Collections.Generic;
using System.Text;

namespace DrawingApp
{
    public interface IGroupable
    {
        public Group GetParent();
    }
}
