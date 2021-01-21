﻿using System.Windows.Forms;

namespace not.Controls
{
    public class MainTabControl : TabControl
    {
        private const string NAME = "MainTabControl";
        private ContextMenuStrip _contextMenuStrip;
        public MainTabControl()
        {
            _contextMenuStrip = new TabControlContextMenuStrip();
            Name = NAME;
            ContextMenuStrip = _contextMenuStrip;
            Dock = DockStyle.Fill;
        }
    }
}