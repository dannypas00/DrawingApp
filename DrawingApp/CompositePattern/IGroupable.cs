using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DrawingApp
{
    public interface IGroupable
    {
        public Group GetParent();
        public void SetParent(Group parent);
        public ListBoxItem GetGroupItem();
        public void SetGroupItem(ListBoxItem groupItem);
        public string GetName();
        public int GetDepth();
    }
}
