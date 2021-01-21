using System.Drawing;
using System.Windows.Forms;

namespace not.Controls
{
    public class CustomTextBox : RichTextBox
    {
        private const string NAME = "RtbTextFileContents";
        public CustomTextBox()
        {
            Name = NAME;
            AcceptsTab = true;
            Font = new Font("Arial", 12.0F, FontStyle.Regular);
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.None;
            ContextMenuStrip = new RichTextBoxContextMenuStrip(this);
        }
    }
}
