using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Migros
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
    public class LabelMenuItem : ToolStripControlHost
    {
        private Label label;

        public LabelMenuItem() : base(new Label() { TextAlign = System.Drawing.ContentAlignment.MiddleRight }) => label = Control as Label;
    }
}