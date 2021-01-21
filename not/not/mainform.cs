using not.Controls;
using not.Objects;
using System.Windows.Forms;

namespace not
{
    public partial class mainform : Form
    {
        public RichTextBox CurrentRtb;
        public TextFile CurrentFile;
        public TabControl MainTabControl;
        public Session Session;

        public mainform()
        {
            InitializeComponent();
            Session = new Session();

            var menuStrip = new MainMenuStrip();
            MainTabControl = new MainTabControl();

            Controls.AddRange(new Control[] { MainTabControl, menuStrip });

            InitializeFile();
        }

        private void InitializeFile()
        {
            if (Session.TextFiles.Count == 0)
            {
                var file = new TextFile("Sans Titre 1");
                var rtb = new CustomTextBox();

                MainTabControl.TabPages.Add(file.SafeFileName);
                var TabPage = MainTabControl.TabPages[0];

                TabPage.Controls.Add(rtb);
                Session.TextFiles.Add(file);
                rtb.Select();

                CurrentFile = file;
                CurrentRtb = rtb;
            }
        }

        private void mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            Session.Save();
        }
    }
}
