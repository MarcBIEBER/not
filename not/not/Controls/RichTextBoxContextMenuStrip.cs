using System.Windows.Forms;

namespace not.Controls
{
    public class RichTextBoxContextMenuStrip : ContextMenuStrip
    {
        private RichTextBox _richTextBox;
        private const string NAME = "RtbContextMenuStrip";

        public RichTextBoxContextMenuStrip(RichTextBox richTextBox)
        {
            Name = NAME;

            _richTextBox = richTextBox;

            var copy = new ToolStripMenuItem("Copier");
            var paste = new ToolStripMenuItem("Coller");
            var cut = new ToolStripMenuItem("Couper");
            var selectAll = new ToolStripMenuItem("Séléctionner tout");

            cut.Click += (s, e) => _richTextBox.Cut();
            copy.Click += (s, e) => _richTextBox.Copy();
            paste.Click += (s, e) => _richTextBox.Paste();
            selectAll.Click += (s, e) => _richTextBox.SelectAll();

            Items.AddRange(new ToolStripItem[] { copy, paste, cut, selectAll });
        }
    }
}
