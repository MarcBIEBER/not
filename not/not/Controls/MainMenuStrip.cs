using not.Objects;
using System.IO;
using System.Windows.Forms;

namespace not.Controls
{
    public class MainMenuStrip : MenuStrip
    {
        private const string NAME = "MainMenuStrip";

        private mainform _form;
        private FontDialog _fontDialogue;
        private OpenFileDialog _openFileDialog;

        public MainMenuStrip()
        {
            Name = NAME;
            Dock = DockStyle.Top;
            _fontDialogue = new FontDialog();
            _openFileDialog = new OpenFileDialog();

            FileDropDownMenu();
            EditDropDownMenu();
            FormatDropDownMenu();
            ViewDropDownMenu();

            HandleCreated += (s, e) =>
            {
                _form = FindForm() as mainform;
            };
        }

        public void FileDropDownMenu()
        {
            var fileDropDownMenu = new ToolStripMenuItem("Fichier");

            var newFile = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            var open = new ToolStripMenuItem("Ouvrir...", null, null, Keys.Control | Keys.O);
            var save = new ToolStripMenuItem("Enregistrer", null, null, Keys.Control | Keys.S);
            var saveAs = new ToolStripMenuItem("Enregistrer sous...", null, null, Keys.Control | Keys.Shift | Keys.S);
            var quit = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            newFile.Click += (s, e) =>
            {
                var tabControl = _form.MainTabControl;
                var TabCount = tabControl.TabCount;

                var fileName = $"Sans titre {TabCount + 1}";
                var file = new TextFile(fileName);
                var rtb = new CustomTextBox();

                tabControl.TabPages.Add(file.SafeFileName);

                var newTabPage = tabControl.TabPages[TabCount];

                newTabPage.Controls.Add(rtb);
                tabControl.SelectedTab = newTabPage;

                _form.Session.TextFiles.Add(file);
                _form.CurrentFile = file;
                _form.CurrentRtb = rtb;
            };

            open.Click += async (s, e) =>
            {
                if (_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var tabControl = _form.MainTabControl;
                    var TabCount = tabControl.TabCount;

                    var file = new TextFile(_openFileDialog.FileName);
                    var rtb = new CustomTextBox();

                    _form.Text = $"{file.FileName}";

                    using (StreamReader reader = new StreamReader(file.FileName))
                    {
                        file.Contents = await reader.ReadToEndAsync();
                    }

                    rtb.Text = file.Contents;

                    tabControl.TabPages.Add(file.SafeFileName);
                    tabControl.TabPages[TabCount].Controls.Add(rtb);

                    _form.Session.TextFiles.Add(file);
                    _form.CurrentRtb = rtb;
                    _form.CurrentFile = file;
                    tabControl.SelectedTab = tabControl.TabPages[TabCount];
                }
            };

            fileDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { newFile, open, save, saveAs, quit });

            Items.Add(fileDropDownMenu);
        }

        public void EditDropDownMenu()
        {
            var editDropDownMenu = new ToolStripMenuItem("Edition");

            var undo = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var redo = new ToolStripMenuItem("Resaurer", null, null, Keys.Control | Keys.Y);

            undo.Click += (s, e) => { if (_form.CurrentRtb.CanUndo) _form.CurrentRtb.Undo(); };
            redo.Click += (s, e) => { if (_form.CurrentRtb.CanRedo) _form.CurrentRtb.Redo(); };

            editDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { undo, redo });
            Items.Add(editDropDownMenu);
        }

        public void FormatDropDownMenu()
        {
            var formatDropDown = new ToolStripMenuItem("Format");
            var font = new ToolStripMenuItem("Police...");
            font.Click += (s, e) =>
            {
                _fontDialogue.Font = _form.CurrentRtb.Font;
                _fontDialogue.ShowDialog();
                _form.CurrentRtb.Font = _fontDialogue.Font;
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
                if (_form.CurrentRtb.ZoomFactor < 3F)
                {
                    _form.CurrentRtb.ZoomFactor += 0.3F;
                }
            };
            zoomOut.Click += (s, e) =>
            {
                if (_form.CurrentRtb.ZoomFactor > 0.7F)
                {
                    _form.CurrentRtb.ZoomFactor -= 0.3F;
                }
            };
            zoomReset.Click += (s, e) => { _form.CurrentRtb.ZoomFactor = 1F; };

            zoomDropDown.DropDownItems.AddRange(new ToolStripItem[] { zoomIn, zoomOut, zoomReset });
            viewDropDown.DropDownItems.AddRange(new ToolStripItem[] { alwaysOnTop, zoomDropDown });

            Items.Add(viewDropDown);
        }
    }
}
