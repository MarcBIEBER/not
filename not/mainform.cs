using not.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace not
{
    public partial class mainform : Form
    {
        public static RichTextBox RichTextBox;
        public mainform()
        {
            InitializeComponent();
    
            var menuStrip = new MainMenuStrip();
            var mainTabControl = new MainTabControl();
            RichTextBox = new CustomTextBox();

            mainTabControl.TabPages.Add("Fichier 1");
            mainTabControl.TabPages[0].Controls.Add(RichTextBox);

            Controls.AddRange(new Control[] { mainTabControl, menuStrip } );
        }
    }
}
