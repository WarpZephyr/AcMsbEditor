using ACMsbEditor.Windows.Forms.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace ACMsbEditor.Windows.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            MainFormMenuStrip = new MenuStrip();
            FileMenuItem = new ToolStripMenuItem();
            OpenMenuItem = new ToolStripMenuItem();
            SaveMenuItem = new ToolStripMenuItem();
            CloseMenuItem = new ToolStripMenuItem();
            OptionsMenuItem = new ToolStripMenuItem();
            ConfirmSaveMenuItem = new ToolStripMenuItem();
            ConfirmCloseMenuItem = new ToolStripMenuItem();
            MaxLogEntriesMenuItem = new ToolStripMenuItem();
            DebugMenuItem = new ToolStripMenuItem();
            TestAc4MsbMenuItem = new ToolStripMenuItem();
            GameComboBox = new ToolStripComboBox();
            FileSplitContainer = new SplitContainer();
            FileListView = new ListView();
            FileContextMenu = new ContextMenuStrip(components);
            ViewFileContextMenu = new ToolStripMenuItem();
            NameFileContextItem = new ToolStripMenuItem();
            GameFileContextItem = new ToolStripMenuItem();
            VersionFileContextItem = new ToolStripMenuItem();
            SaveFileContextMenu = new ToolStripMenuItem();
            CloseFileContextMenu = new ToolStripMenuItem();
            EntrySplitContainer = new SplitContainer();
            MsbParamView = new TreeView();
            MsbParamContextMenu = new ContextMenuStrip(components);
            NewMsbParamContextMenu = new ToolStripMenuItem();
            DeleteMsbParamContextMenu = new ToolStripMenuItem();
            DuplicateMsbParamContextMenu = new ToolStripMenuItem();
            LogSplitContainer = new SplitContainer();
            LogListBox = new ScrollingListBox();
            LogContextMenu = new ContextMenuStrip(components);
            CopyLogContextMenu = new ToolStripMenuItem();
            ClearLogContextMenu = new ToolStripMenuItem();
            ConfirmEntryDeleteMenuItem = new ToolStripMenuItem();
            MainFormMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FileSplitContainer).BeginInit();
            FileSplitContainer.Panel1.SuspendLayout();
            FileSplitContainer.Panel2.SuspendLayout();
            FileSplitContainer.SuspendLayout();
            FileContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EntrySplitContainer).BeginInit();
            EntrySplitContainer.Panel1.SuspendLayout();
            EntrySplitContainer.SuspendLayout();
            MsbParamContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogSplitContainer).BeginInit();
            LogSplitContainer.Panel1.SuspendLayout();
            LogSplitContainer.Panel2.SuspendLayout();
            LogSplitContainer.SuspendLayout();
            LogContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // MainFormMenuStrip
            // 
            MainFormMenuStrip.BackColor = SystemColors.ControlLight;
            MainFormMenuStrip.Items.AddRange(new ToolStripItem[] { FileMenuItem, OptionsMenuItem, DebugMenuItem, GameComboBox });
            MainFormMenuStrip.Location = new Point(0, 0);
            MainFormMenuStrip.Name = "MainFormMenuStrip";
            MainFormMenuStrip.Size = new Size(984, 27);
            MainFormMenuStrip.TabIndex = 0;
            MainFormMenuStrip.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            FileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { OpenMenuItem, SaveMenuItem, CloseMenuItem });
            FileMenuItem.Name = "FileMenuItem";
            FileMenuItem.Size = new Size(37, 23);
            FileMenuItem.Text = "File";
            // 
            // OpenMenuItem
            // 
            OpenMenuItem.Name = "OpenMenuItem";
            OpenMenuItem.Size = new Size(145, 22);
            OpenMenuItem.Text = "Open";
            OpenMenuItem.Click += OpenMenuItem_Click;
            // 
            // SaveMenuItem
            // 
            SaveMenuItem.Name = "SaveMenuItem";
            SaveMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            SaveMenuItem.Size = new Size(145, 22);
            SaveMenuItem.Text = "Save";
            SaveMenuItem.Click += SaveMenuItem_Click;
            // 
            // CloseMenuItem
            // 
            CloseMenuItem.Name = "CloseMenuItem";
            CloseMenuItem.ShortcutKeyDisplayString = "Ctrl+D";
            CloseMenuItem.Size = new Size(145, 22);
            CloseMenuItem.Text = "Close";
            CloseMenuItem.Click += CloseMenuItem_Click;
            // 
            // OptionsMenuItem
            // 
            OptionsMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ConfirmSaveMenuItem, ConfirmCloseMenuItem, ConfirmEntryDeleteMenuItem, MaxLogEntriesMenuItem });
            OptionsMenuItem.Name = "OptionsMenuItem";
            OptionsMenuItem.Size = new Size(61, 23);
            OptionsMenuItem.Text = "Options";
            // 
            // ConfirmSaveMenuItem
            // 
            ConfirmSaveMenuItem.Checked = true;
            ConfirmSaveMenuItem.CheckOnClick = true;
            ConfirmSaveMenuItem.CheckState = CheckState.Checked;
            ConfirmSaveMenuItem.Name = "ConfirmSaveMenuItem";
            ConfirmSaveMenuItem.Size = new Size(184, 22);
            ConfirmSaveMenuItem.Text = "Confirm Save";
            ConfirmSaveMenuItem.ToolTipText = "Whether or not to confirm before saving";
            ConfirmSaveMenuItem.CheckedChanged += ConfirmSaveMenuItem_CheckedChanged;
            // 
            // ConfirmCloseMenuItem
            // 
            ConfirmCloseMenuItem.Checked = true;
            ConfirmCloseMenuItem.CheckOnClick = true;
            ConfirmCloseMenuItem.CheckState = CheckState.Checked;
            ConfirmCloseMenuItem.Name = "ConfirmCloseMenuItem";
            ConfirmCloseMenuItem.Size = new Size(184, 22);
            ConfirmCloseMenuItem.Text = "Confirm Close";
            ConfirmCloseMenuItem.ToolTipText = "Whether or not to confirm before closing";
            ConfirmCloseMenuItem.CheckedChanged += ConfirmCloseMenuItem_CheckedChanged;
            // 
            // MaxLogEntriesMenuItem
            // 
            MaxLogEntriesMenuItem.Name = "MaxLogEntriesMenuItem";
            MaxLogEntriesMenuItem.Size = new Size(184, 22);
            MaxLogEntriesMenuItem.Text = "Max Log Entries";
            MaxLogEntriesMenuItem.Click += MaxLogEntriesMenuItem_Click;
            // 
            // DebugMenuItem
            // 
            DebugMenuItem.DropDownItems.AddRange(new ToolStripItem[] { TestAc4MsbMenuItem });
            DebugMenuItem.Name = "DebugMenuItem";
            DebugMenuItem.Size = new Size(54, 23);
            DebugMenuItem.Text = "Debug";
            // 
            // TestAc4MsbMenuItem
            // 
            TestAc4MsbMenuItem.Name = "TestAc4MsbMenuItem";
            TestAc4MsbMenuItem.Size = new Size(180, 22);
            TestAc4MsbMenuItem.Text = "Test AC4 MSB";
            TestAc4MsbMenuItem.Click += TestAc4MsbMenuItem_Click;
            // 
            // GameComboBox
            // 
            GameComboBox.Alignment = ToolStripItemAlignment.Right;
            GameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            GameComboBox.Name = "GameComboBox";
            GameComboBox.Size = new Size(121, 23);
            GameComboBox.SelectedIndexChanged += GameComboBox_SelectedIndexChanged;
            // 
            // FileSplitContainer
            // 
            FileSplitContainer.AllowDrop = true;
            FileSplitContainer.Dock = DockStyle.Fill;
            FileSplitContainer.Location = new Point(0, 0);
            FileSplitContainer.Name = "FileSplitContainer";
            // 
            // FileSplitContainer.Panel1
            // 
            FileSplitContainer.Panel1.BackColor = SystemColors.ControlLightLight;
            FileSplitContainer.Panel1.Controls.Add(FileListView);
            // 
            // FileSplitContainer.Panel2
            // 
            FileSplitContainer.Panel2.Controls.Add(EntrySplitContainer);
            FileSplitContainer.Size = new Size(984, 488);
            FileSplitContainer.SplitterDistance = 304;
            FileSplitContainer.TabIndex = 1;
            FileSplitContainer.DragDrop += FileSplitContainer_DragDrop;
            FileSplitContainer.DragEnter += FileSplitContainer_DragEnter;
            // 
            // FileListView
            // 
            FileListView.AllowColumnReorder = true;
            FileListView.BorderStyle = BorderStyle.None;
            FileListView.ContextMenuStrip = FileContextMenu;
            FileListView.Dock = DockStyle.Fill;
            FileListView.Location = new Point(0, 0);
            FileListView.Name = "FileListView";
            FileListView.Size = new Size(304, 488);
            FileListView.TabIndex = 0;
            FileListView.UseCompatibleStateImageBehavior = false;
            FileListView.View = View.Details;
            FileListView.ColumnClick += FileListView_ColumnClick;
            FileListView.ColumnWidthChanged += FileListView_ColumnWidthChanged;
            FileListView.ItemSelectionChanged += FileListView_ItemSelectionChanged;
            FileListView.KeyDown += FileListView_KeyDown;
            // 
            // FileContextMenu
            // 
            FileContextMenu.Items.AddRange(new ToolStripItem[] { ViewFileContextMenu, SaveFileContextMenu, CloseFileContextMenu });
            FileContextMenu.Name = "FileContextMenu";
            FileContextMenu.Size = new Size(146, 70);
            // 
            // ViewFileContextMenu
            // 
            ViewFileContextMenu.DropDownItems.AddRange(new ToolStripItem[] { NameFileContextItem, GameFileContextItem, VersionFileContextItem });
            ViewFileContextMenu.Name = "ViewFileContextMenu";
            ViewFileContextMenu.Size = new Size(145, 22);
            ViewFileContextMenu.Text = "View";
            // 
            // NameFileContextItem
            // 
            NameFileContextItem.Checked = true;
            NameFileContextItem.CheckOnClick = true;
            NameFileContextItem.CheckState = CheckState.Checked;
            NameFileContextItem.Name = "NameFileContextItem";
            NameFileContextItem.Size = new Size(112, 22);
            NameFileContextItem.Text = "Name";
            NameFileContextItem.CheckedChanged += NameFileContextItem_CheckedChanged;
            // 
            // GameFileContextItem
            // 
            GameFileContextItem.Checked = true;
            GameFileContextItem.CheckOnClick = true;
            GameFileContextItem.CheckState = CheckState.Checked;
            GameFileContextItem.Name = "GameFileContextItem";
            GameFileContextItem.Size = new Size(112, 22);
            GameFileContextItem.Text = "Game";
            GameFileContextItem.CheckedChanged += GameFileContextItem_CheckedChanged;
            // 
            // VersionFileContextItem
            // 
            VersionFileContextItem.Checked = true;
            VersionFileContextItem.CheckOnClick = true;
            VersionFileContextItem.CheckState = CheckState.Checked;
            VersionFileContextItem.Name = "VersionFileContextItem";
            VersionFileContextItem.Size = new Size(112, 22);
            VersionFileContextItem.Text = "Version";
            VersionFileContextItem.CheckedChanged += VersionFileContextItem_CheckedChanged;
            // 
            // SaveFileContextMenu
            // 
            SaveFileContextMenu.Name = "SaveFileContextMenu";
            SaveFileContextMenu.ShortcutKeyDisplayString = "Ctrl+S";
            SaveFileContextMenu.Size = new Size(145, 22);
            SaveFileContextMenu.Text = "Save";
            SaveFileContextMenu.Click += SaveFileContextMenu_Click;
            // 
            // CloseFileContextMenu
            // 
            CloseFileContextMenu.Name = "CloseFileContextMenu";
            CloseFileContextMenu.ShortcutKeyDisplayString = "Ctrl+D";
            CloseFileContextMenu.Size = new Size(145, 22);
            CloseFileContextMenu.Text = "Close";
            CloseFileContextMenu.Click += CloseFileContextMenu_Click;
            // 
            // EntrySplitContainer
            // 
            EntrySplitContainer.Dock = DockStyle.Fill;
            EntrySplitContainer.Location = new Point(0, 0);
            EntrySplitContainer.Name = "EntrySplitContainer";
            // 
            // EntrySplitContainer.Panel1
            // 
            EntrySplitContainer.Panel1.BackColor = SystemColors.ControlLightLight;
            EntrySplitContainer.Panel1.Controls.Add(MsbParamView);
            // 
            // EntrySplitContainer.Panel2
            // 
            EntrySplitContainer.Panel2.BackColor = SystemColors.ControlLightLight;
            EntrySplitContainer.Size = new Size(676, 488);
            EntrySplitContainer.SplitterDistance = 335;
            EntrySplitContainer.TabIndex = 0;
            // 
            // MsbParamView
            // 
            MsbParamView.BorderStyle = BorderStyle.None;
            MsbParamView.ContextMenuStrip = MsbParamContextMenu;
            MsbParamView.Dock = DockStyle.Fill;
            MsbParamView.Location = new Point(0, 0);
            MsbParamView.Name = "MsbParamView";
            MsbParamView.Size = new Size(335, 488);
            MsbParamView.TabIndex = 0;
            // 
            // MsbParamContextMenu
            // 
            MsbParamContextMenu.Items.AddRange(new ToolStripItem[] { NewMsbParamContextMenu, DeleteMsbParamContextMenu, DuplicateMsbParamContextMenu });
            MsbParamContextMenu.Name = "MsbParamContextMenu";
            MsbParamContextMenu.Size = new Size(199, 70);
            // 
            // NewMsbParamContextMenu
            // 
            NewMsbParamContextMenu.Name = "NewMsbParamContextMenu";
            NewMsbParamContextMenu.ShortcutKeyDisplayString = "Ctrl+N";
            NewMsbParamContextMenu.Size = new Size(198, 22);
            NewMsbParamContextMenu.Text = "New";
            NewMsbParamContextMenu.Click += NewMsbParamContextMenu_Click;
            // 
            // DeleteMsbParamContextMenu
            // 
            DeleteMsbParamContextMenu.Name = "DeleteMsbParamContextMenu";
            DeleteMsbParamContextMenu.ShortcutKeyDisplayString = "Ctrl+D";
            DeleteMsbParamContextMenu.Size = new Size(198, 22);
            DeleteMsbParamContextMenu.Text = "Delete";
            DeleteMsbParamContextMenu.Click += DeleteMsbParamContextMenu_Click;
            // 
            // DuplicateMsbParamContextMenu
            // 
            DuplicateMsbParamContextMenu.Name = "DuplicateMsbParamContextMenu";
            DuplicateMsbParamContextMenu.ShortcutKeyDisplayString = "Ctrl+Shift+C";
            DuplicateMsbParamContextMenu.Size = new Size(198, 22);
            DuplicateMsbParamContextMenu.Text = "Duplicate";
            DuplicateMsbParamContextMenu.Click += DuplicateMsbParamContextMenu_Click;
            // 
            // LogSplitContainer
            // 
            LogSplitContainer.Dock = DockStyle.Fill;
            LogSplitContainer.Location = new Point(0, 27);
            LogSplitContainer.Name = "LogSplitContainer";
            LogSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // LogSplitContainer.Panel1
            // 
            LogSplitContainer.Panel1.Controls.Add(FileSplitContainer);
            // 
            // LogSplitContainer.Panel2
            // 
            LogSplitContainer.Panel2.Controls.Add(LogListBox);
            LogSplitContainer.Size = new Size(984, 578);
            LogSplitContainer.SplitterDistance = 488;
            LogSplitContainer.TabIndex = 0;
            // 
            // LogListBox
            // 
            LogListBox.BorderStyle = BorderStyle.FixedSingle;
            LogListBox.ContextMenuStrip = LogContextMenu;
            LogListBox.Dock = DockStyle.Fill;
            LogListBox.HorizontalScrollbar = true;
            LogListBox.IntegralHeight = false;
            LogListBox.ItemHeight = 15;
            LogListBox.Location = new Point(0, 0);
            LogListBox.Name = "LogListBox";
            LogListBox.SelectionMode = SelectionMode.MultiExtended;
            LogListBox.Size = new Size(984, 86);
            LogListBox.TabIndex = 0;
            LogListBox.KeyDown += LogListBox_KeyDown;
            // 
            // LogContextMenu
            // 
            LogContextMenu.Items.AddRange(new ToolStripItem[] { CopyLogContextMenu, ClearLogContextMenu });
            LogContextMenu.Name = "LogContextMenu";
            LogContextMenu.Size = new Size(145, 48);
            // 
            // CopyLogContextMenu
            // 
            CopyLogContextMenu.Name = "CopyLogContextMenu";
            CopyLogContextMenu.ShortcutKeyDisplayString = "Ctrl+C";
            CopyLogContextMenu.Size = new Size(144, 22);
            CopyLogContextMenu.Text = "Copy";
            CopyLogContextMenu.Click += CopyLogContextMenu_Click;
            // 
            // ClearLogContextMenu
            // 
            ClearLogContextMenu.Name = "ClearLogContextMenu";
            ClearLogContextMenu.Size = new Size(144, 22);
            ClearLogContextMenu.Text = "Clear";
            ClearLogContextMenu.Click += ClearLogContextMenu_Click;
            // 
            // ConfirmEntryDeleteMenuItem
            // 
            ConfirmEntryDeleteMenuItem.Checked = true;
            ConfirmEntryDeleteMenuItem.CheckOnClick = true;
            ConfirmEntryDeleteMenuItem.CheckState = CheckState.Checked;
            ConfirmEntryDeleteMenuItem.Name = "ConfirmEntryDeleteMenuItem";
            ConfirmEntryDeleteMenuItem.Size = new Size(184, 22);
            ConfirmEntryDeleteMenuItem.Text = "Confirm Entry Delete";
            ConfirmEntryDeleteMenuItem.CheckedChanged += ConfirmEntryDeleteMenuItem_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 605);
            Controls.Add(LogSplitContainer);
            Controls.Add(MainFormMenuStrip);
            DoubleBuffered = true;
            MainMenuStrip = MainFormMenuStrip;
            Name = "MainForm";
            Text = "AcMsbEditor";
            MainFormMenuStrip.ResumeLayout(false);
            MainFormMenuStrip.PerformLayout();
            FileSplitContainer.Panel1.ResumeLayout(false);
            FileSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)FileSplitContainer).EndInit();
            FileSplitContainer.ResumeLayout(false);
            FileContextMenu.ResumeLayout(false);
            EntrySplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)EntrySplitContainer).EndInit();
            EntrySplitContainer.ResumeLayout(false);
            MsbParamContextMenu.ResumeLayout(false);
            LogSplitContainer.Panel1.ResumeLayout(false);
            LogSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)LogSplitContainer).EndInit();
            LogSplitContainer.ResumeLayout(false);
            LogContextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MainFormMenuStrip;
        private ToolStripMenuItem FileMenuItem;
        private ToolStripMenuItem OpenMenuItem;
        private ToolStripMenuItem DebugMenuItem;
        private ToolStripMenuItem TestAc4MsbMenuItem;
        private SplitContainer FileSplitContainer;
        private SplitContainer EntrySplitContainer;
        private ListView FileListView;
        private ToolStripComboBox GameComboBox;
        private SplitContainer LogSplitContainer;
        private ScrollingListBox LogListBox;
        private ToolStripMenuItem SaveMenuItem;
        private ToolStripMenuItem CloseMenuItem;
        private ToolStripMenuItem OptionsMenuItem;
        private ToolStripMenuItem ConfirmSaveMenuItem;
        private ToolStripMenuItem ConfirmCloseMenuItem;
        private ToolStripMenuItem MaxLogEntriesMenuItem;
        private ContextMenuStrip FileContextMenu;
        private ToolStripMenuItem SaveFileContextMenu;
        private ToolStripMenuItem CloseFileContextMenu;
        private ContextMenuStrip LogContextMenu;
        private ToolStripMenuItem CopyLogContextMenu;
        private ToolStripMenuItem ClearLogContextMenu;
        private ToolStripMenuItem ViewFileContextMenu;
        private ToolStripMenuItem NameFileContextItem;
        private ToolStripMenuItem GameFileContextItem;
        private ToolStripMenuItem VersionFileContextItem;
        private TreeView MsbParamView;
        private ContextMenuStrip MsbParamContextMenu;
        private ToolStripMenuItem NewMsbParamContextMenu;
        private ToolStripMenuItem DeleteMsbParamContextMenu;
        private ToolStripMenuItem DuplicateMsbParamContextMenu;
        private ToolStripMenuItem ConfirmEntryDeleteMenuItem;
    }
}
