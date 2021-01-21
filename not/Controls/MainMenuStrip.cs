using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace not.Controls
{
    public class MainMenuStrip : MenuStrip
    {
        private const string NAME = "MainMenuStrip";
        private FontDialog _fontDialogue;

        public MainMenuStrip()
        {
            Name = NAME;
            Dock = DockStyle.Top;
            _fontDialogue = new FontDialog();
            FileDropDownMenu();
            EditDropDownMenu();
            FormatDropDownMenu();
            ViewDropDownMenu();
        }

        public void FileDropDownMenu()
        {
            var fileDropDownMenu = new ToolStripMenuItem("Fichier");

            var newMenu = new ToolStripMenuItem("Fichier", null, null, Keys.Control | Keys.N);
            var openMenu = new ToolStripMenuItem("Ouvrir...", null, null, Keys.Control | Keys.O);
            var saveMenu = new ToolStripMenuItem("Enregistrer", null, null, Keys.Control | Keys.S);
            var saveAsMenu = new ToolStripMenuItem("Enregistrer sous...", null, null, Keys.Control | Keys.Shift | Keys.S);
            var quitMenu = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            fileDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { newMenu, openMenu, saveMenu, saveAsMenu, quitMenu } );

            Items.Add(fileDropDownMenu);
        }

        public void EditDropDownMenu()
        {
            var editDropDownMenu = new ToolStripMenuItem("Edition");

            var undo = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var redo = new ToolStripMenuItem("Resaurer", null, null, Keys.Control | Keys.Y);

            undo.Click += (s, e) => { if (mainform.RichTextBox.CanUndo) mainform.RichTextBox.Undo(); };
            redo.Click += (s, e) => { if (mainform.RichTextBox.CanRedo) mainform.RichTextBox.Redo(); };

            editDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { undo, redo } );
            Items.Add(editDropDownMenu);
        }

        public void FormatDropDownMenu()
        {
            var formatDropDown = new ToolStripMenuItem("Format");
            var font = new ToolStripMenuItem("Police...");
            font.Click += (s, e) =>
            {
                _fontDialogue.Font = mainform.RichTextBox.Font;
                _fontDialogue.ShowDialog();
                mainform.RichTextBox.Font = _fontDialogue.Font;
            };
            formatDropDown.DropDownItems.AddRange(new ToolStripItem[] { font });

            Items.Add(formatDropDown);
        }

        public void ViewDropDownMenu()
        {
            var viewDropDown = new ToolStripMenuItem("Affichage");
            var alwaysOnTop = new ToolStripMenuItem("Toujours devant");

            var zoomDropDown = new ToolStripMenuItem("Zoom");
            var zoomIn = new ToolStripMenuItem("Zoom avant", null, null, Keys.Control | Keys.Add);
            var zoomOut = new ToolStripMenuItem("Zoom arrière", null, null, Keys.Control | Keys.Subtract);
            var zoomReset = new ToolStripMenuItem("Par défaut", null, null, Keys.Control | Keys.Divide);

            zoomIn.ShortcutKeyDisplayString = "Ctrl+Num +";
            zoomOut.ShortcutKeyDisplayString = "Ctrl+Num -";
            zoomReset.ShortcutKeyDisplayString = "Ctrl+Num /";

            alwaysOnTop.Click += (s, e) =>
            {
                if (alwaysOnTop.Checked)
                {
                    alwaysOnTop.Checked = false;
                    Program.MainForm.TopMost = false;
                }
                else
                {
                    alwaysOnTop.Checked = true;
                    Program.MainForm.TopMost = true;
                }
            };

            zoomIn.Click += (s, e) =>
            {
                if (mainform.RichTextBox.ZoomFactor < 3F)
                {
                    mainform.RichTextBox.ZoomFactor += 0.3F;
                }
            };
            zoomOut.Click += (s, e) =>
            {
                if (mainform.RichTextBox.ZoomFactor > 0.7F)
                {
                    mainform.RichTextBox.ZoomFactor -= 0.3F;
                }
            };
            zoomReset.Click += (s, e) => { mainform.RichTextBox.ZoomFactor = 1F; };

            zoomDropDown.DropDownItems.AddRange(new ToolStripItem[] { zoomIn, zoomOut, zoomReset });
            viewDropDown.DropDownItems.AddRange(new ToolStripItem[] { alwaysOnTop, zoomDropDown });

            Items.Add(viewDropDown);
        }
    }
}
