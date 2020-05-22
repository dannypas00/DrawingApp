using System.Drawing;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DrawingApp.CompositePattern
{
    public class CanvasShape : IGroupable
    {
        #pragma warning disable 414
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0052:Remove unread private members", Justification = "Might be used later on, will remove if unneeded before production")]
        private bool selected = false;
        #pragma warning restore 414
        private readonly Shape shape;
        private Group parent;
        private readonly string name = "";
        private readonly int depth = 0;
        private ListBoxItem groupItem;

        public CanvasShape(Shape shape, Group parent)
        {
            this.shape = shape;
            this.parent = parent;
            if (shape is System.Windows.Shapes.Rectangle)
            {
                name = parent.GetGroupItem().Content.ToString()?.Split(' ')[0] + " rectangle";
            }
            else if (shape is Ellipse)
            {
                name = parent.GetGroupItem().Content.ToString()?.Split(' ')[0] + " ellipse";
            }
            Group previewParent = parent;
            while (previewParent != null)
            {
                depth++;
                previewParent = previewParent.GetParent();
            }
        }

        public void Unselect()
        {
            selected = false;
            shape.StrokeDashArray = null;
        }

        public CanvasShape Select()
        {
            selected = true;
            shape.StrokeDashArray = new System.Windows.Media.DoubleCollection() { 1 };
            return this;
        }

        public Shape GetShape()
        {
            return shape;
        }

        public Group GetParent()
        {
            return parent;
        }

        public void SetParent(Group parent)
        {
            this.parent = parent;
        }

        public ListBoxItem GetGroupItem()
        {
            return groupItem;
        }

        public void SetGroupItem(ListBoxItem groupItem)
        {
            this.groupItem = groupItem;
        }

        public string GetName()
        {
            return name;
        }

        public int GetDepth()
        {
            return depth;
        }
    }
}
