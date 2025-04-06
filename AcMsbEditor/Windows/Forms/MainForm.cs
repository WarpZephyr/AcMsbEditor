using AcMsbEditor.Binding.Nodes;
using AcMsbEditor.Windows.Forms.Dialogs.IO;
using ACMsbEditor.Binding;
using ACMsbEditor.Binding.Msb;
using ACMsbEditor.Tests;
using ACMsbEditor.Windows.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ACMsbEditor.Windows.Forms
{
    public partial class MainForm : Form
    {
        private readonly AppConfig Config;
        private readonly string ConfigPath;
        private readonly MsbFileInterface MsbFileInterface;
        private readonly ListViewColumnSorter FileListViewColumnSorter;
        private IMsbNodeBinding? CurrentMsbNodeBinding;

        public MainForm(AppConfig config, string configPath)
        {
            // Set config
            Config = config;
            ConfigPath = configPath;

            // Initialize WinForms UI
            InitializeComponent();

            // Setup file interface
            MsbFileInterface = new MsbFileInterface();

            // Setup file list view columns
            var nameColumn = new ColumnHeader()
            {
                Width = Config.ShowNameFileList ? Config.WidthFileNameColumn : 0,
                Name = "Name",
                Text = "Name"
            };

            var gameColumn = new ColumnHeader()
            {
                Width = Config.ShowGameFileList ? Config.WidthFileGameColumn : 0,
                Name = "Game",
                Text = "Game"
            };

            var versionColumn = new ColumnHeader()
            {
                Width = Config.ShowVersionFileList ? Config.WidthFileVersionColumn : 0,
                Name = "Version",
                Text = "Version"
            };

            // Add file list view columns
            FileListView.Columns.Add(nameColumn);
            FileListView.Columns.Add(gameColumn);
            FileListView.Columns.Add(versionColumn);

            // Setup file list view column sorter
            FileListViewColumnSorter = new ListViewColumnSorter();
            FileListView.ListViewItemSorter = FileListViewColumnSorter;

            // Setup check settings
            ConfirmSaveMenuItem.Checked = Config.ConfirmSave;
            ConfirmCloseMenuItem.Checked = Config.ConfirmClose;
            ConfirmEntryDeleteMenuItem.Checked = Config.ConfirmEntryDelete;
            NameFileContextItem.Checked = Config.ShowNameFileList;
            GameFileContextItem.Checked = Config.ShowGameFileList;
            VersionFileContextItem.Checked = Config.ShowVersionFileList;

            // Setup game combobox items
            GameComboBox.Items.AddRange(EnumCache<MsbGame>.GetEnumNames());
            if (EnumCache<MsbGame>.TryGetEnumIndex(config.SelectedMsbGame, out int index))
            {
                GameComboBox.SelectedIndex = index;
            }
            else
            {
                GameComboBox.SelectedIndex = 0;
                LogError($"Failed to get index of {nameof(MsbGame)} config value: {config.SelectedMsbGame}");
                LogWarn($"Setting default {nameof(MsbGame)} config value: {MsbGame.AC4}");
                Config.SelectedMsbGame = MsbGame.AC4;
                SaveConfig();
            }
        }

        #region File Menu

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            PromptOpen();
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            PromptSave();
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            PromptClose();
        }

        #endregion

        #region Options Menu

        private void ConfirmSaveMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Config.ConfirmSave = ConfirmSaveMenuItem.Checked;
            SaveConfig();
        }

        private void ConfirmCloseMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Config.ConfirmClose = ConfirmCloseMenuItem.Checked;
            SaveConfig();
        }

        private void ConfirmEntryDeleteMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Config.ConfirmEntryDelete = ConfirmEntryDeleteMenuItem.Checked;
            SaveConfig();
        }

        private void MaxLogEntriesMenuItem_Click(object sender, EventArgs e)
        {
            decimal result = SimpleFormDialog.ShowNumericInputDialog(Config.MaxLogListBoxEntries, "Input the max log entry value.", "Input");
            result = Math.Abs(result);
            result = Math.Clamp(result, 0, int.MaxValue);
            result = Math.Floor(result);

            Config.MaxLogListBoxEntries = (int)result;
            SaveConfig();
        }

        #endregion

        #region Debug Test Menu

        private void TestAc4MsbMenuItem_Click(object sender, EventArgs e)
        {
            string? directory = DirectoryDialog.GetFolderPath(null, "Select a folder with AC4 MSB");
            if (!string.IsNullOrEmpty(directory))
            {
                MsbTest.TestAC4(directory);
            }
        }

        #endregion

        #region Game ComboBox

        private void GameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.SelectedMsbGame = EnumCache<MsbGame>.GetEnumValue(GameComboBox.Text);
            SaveConfig();
        }

        #endregion

        #region File View

        private bool TryGetFileInfo(ListViewItem item, [NotNullWhen(true)] out MsbFileInfo? msbFile)
        {
            var file = item.Tag as MsbFileInfo;
            if (file != null)
            {
                msbFile = file;
                return true;
            }
            else
            {
                LogError($"File list item {item} has null data, unable to get file info.");
                msbFile = null;
                return false;
            }
        }

        private List<MsbFileInfo> GetSelectedFileInfo()
        {
            var selected = FileListView.SelectedItems;
            var files = new List<MsbFileInfo>();

            foreach (ListViewItem item in selected)
            {
                if (TryGetFileInfo(item, out MsbFileInfo? msbFile))
                {
                    files.Add(msbFile);
                }
            }

            return files;
        }

        private List<MsbFileInfo> CutSelectedFileInfo()
        {
            var selected = FileListView.SelectedItems;
            var files = new List<MsbFileInfo>();

            int index = 0;
            foreach (ListViewItem item in selected)
            {
                if (TryGetFileInfo(item, out MsbFileInfo? msbFile))
                {
                    files.Add(msbFile);
                    FileListView.Items.Remove(item);
                }

                index++;
            }

            ClearMsbParamView();
            return files;
        }

        private void LoadFileView(MsbFileInfo info)
        {
            var item = new ListViewItem([info.Name, info.Game.ToString(), info.Version.ToString()])
            {
                Tag = info
            };

            FileListView.Items.Add(item);
        }

        private void RemoveFileView(MsbFileInfo info)
        {
            foreach (ListViewItem item in FileListView.Items)
            {
                if (item.Tag != null
                    && item.Tag.Equals(info))
                {
                    FileListView.Items.Remove(item);
                    break;
                }
            }
        }

        private void FileListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            FileListViewColumnSort(e.Column);
        }

        private void FileListViewColumnSort(int column)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (column == FileListViewColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (FileListViewColumnSorter.Order == SortOrder.Ascending)
                {
                    FileListViewColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    FileListViewColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                FileListViewColumnSorter.SortColumn = column;
                FileListViewColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            FileListView.Sort();
        }

        #endregion

        #region File View Columns

        private bool HideFileViewColumn(string name, int width, bool show)
        {
            var column = FileListView.Columns[name];
            if (column == null)
            {
                LogError($"Failed trying to get column by name: {name}");
                return false;
            }

            if (show)
            {
                column.Width = width;
                LogInfo($"Showing {name} file view column.");
            }
            else
            {
                column.Width = 0;
                LogInfo($"Hiding {name} file view column.");
            }

            return true;
        }

        private void NameFileContextItem_CheckedChanged(object sender, EventArgs e)
        {
            bool show = NameFileContextItem.Checked;
            if (HideFileViewColumn("Name", Config.WidthFileNameColumn, show))
            {
                Config.ShowNameFileList = show;
                SaveConfig();
            }
        }

        private void GameFileContextItem_CheckedChanged(object sender, EventArgs e)
        {
            bool show = GameFileContextItem.Checked;
            if (HideFileViewColumn("Game", Config.WidthFileGameColumn, show))
            {
                Config.ShowGameFileList = show;
                SaveConfig();
            }
        }

        private void VersionFileContextItem_CheckedChanged(object sender, EventArgs e)
        {
            bool show = VersionFileContextItem.Checked;
            if (HideFileViewColumn("Version", Config.WidthFileVersionColumn, show))
            {
                Config.ShowVersionFileList = show;
                SaveConfig();
            }
        }

        private void FileListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            var column = FileListView.Columns[e.ColumnIndex];
            switch (column.Name)
            {
                case "Name":
                    Config.WidthFileNameColumn = column.Width;
                    SaveConfig();
                    break;
                case "Game":
                    Config.WidthFileGameColumn = column.Width;
                    SaveConfig();
                    break;
                case "Version":
                    Config.WidthFileVersionColumn = column.Width;
                    SaveConfig();
                    break;
                default:
                    LogError($"Unable to save width, unknown column: {column.Name}");
                    break;
            }
        }

        #endregion

        #region File View Selection

        private void FileListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item == null)
                return;

            UpdateMsbParamViewFile(e.Item);
        }

        #endregion

        #region File Context Menu

        private void SaveFileContextMenu_Click(object sender, EventArgs e)
        {
            SaveMenuItem_Click(sender, e);
        }

        private void CloseFileContextMenu_Click(object sender, EventArgs e)
        {
            CloseMenuItem_Click(sender, e);
        }

        #endregion

        #region File Key Down

        private void FileListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        SaveMenuItem_Click(sender, e);
                        break;
                    case Keys.D:
                        CloseMenuItem_Click(sender, e);
                        break;
                }
            }
        }

        #endregion

        #region File Drag Drop

        private void FileSplitContainer_DragEnter(object sender, DragEventArgs e)
        {
            var data = e.Data;
            if (data == null)
                return;

            if (data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void FileSplitContainer_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data;
            if (data == null)
                return;

            object? droppedData = data.GetData(DataFormats.FileDrop);
            if (droppedData is string[] paths)
            {
                OpenFiles(paths);
            }
            else
            {
                LogError("Drag and drop data invalid.");
            }
        }

        #endregion

        #region Msb Param View

        private void UpdateMsbParamViewFile(ListViewItem item)
        {
            if (!TryGetFileInfo(item, out MsbFileInfo? msbFile))
            {
                LogError("Unable to get file info, cannot change viewed file.");
                return;
            }

            if (!MsbFileInterface.GetMsbNodeBinding(msbFile.FilePath, out IMsbNodeBinding? binding))
            {
                LogError($"Unable to find binding for {msbFile.Name}, cannot change viewed file.");
                return;
            }

            RepopulateMsbParamView(binding);
        }

        private void RepopulateMsbParamView(IMsbNodeBinding binding)
        {
            MsbParamView.BeginUpdate();
            MsbParamView.Nodes.Clear();
            var paramEntries = binding.GetMsbParams();
            foreach (var paramEntry in paramEntries)
            {
                var paramNode = new TreeNode(paramEntry.Name)
                {
                    Tag = paramEntry
                };

                var entries = paramEntry.Entries;
                foreach (var entry in entries)
                {
                    var entryNode = new TreeNode(entry.Name)
                    {
                        Tag = entry
                    };

                    paramNode.Nodes.Add(entryNode);
                }

                MsbParamView.Nodes.Add(paramNode);
            }

            MsbParamView.Tag = binding;
            CurrentMsbNodeBinding = binding;
            MsbParamView.EndUpdate();
        }

        private void ClearMsbParamView()
        {
            MsbParamView.BeginUpdate();
            MsbParamView.Nodes.Clear();
            MsbParamView.Tag = null;
            MsbParamView.EndUpdate();
        }

        private bool GetSelectedMsbEntry([NotNullWhen(true)] out MsbEntryNode? entry)
        {
            if (MsbParamView.SelectedNode == null)
            {
                LogWarn("No entries are currently selected.");
                entry = null;
                return false;
            }
            
            var tag = MsbParamView.SelectedNode.Tag;
            if (tag == null)
            {
                LogError("Data for selected entry is null.");
                entry = null;
                return false;
            }

            if (tag is MsbParamNode param)
            {
                LogWarn("Selected item is a section, not an entry.");
                entry = null;
                return false;
            }

            if (tag is MsbEntryNode entryNode)
            {
                entry = entryNode;
                return true;
            }

            LogError($"Selected item data is unknown: {tag.GetType().Name}");
            entry = null;
            return false;
        }

        private void NewMsbEntry()
        {
            if (CurrentMsbNodeBinding == null)
            {
                LogWarn($"No MSB is currently selected, cannot make new entry.");
                return;
            }

            var binding = CurrentMsbNodeBinding;
            string[] paramTypes = binding.GetMsbParamTypes();
            string? selectedParamType = SimpleFormDialog.ShowComboBoxDialog("Select param.", "Select", paramTypes, ComboBoxStyle.DropDownList);
            if (selectedParamType == null)
            {
                LogInfo("No section selected, cancelling new operation.");
                return;
            }

            if (!binding.GetMsbEntryTypes(selectedParamType, out string[]? entryTypes))
            {
                LogError($"Could not get entry types for the selected section: {selectedParamType}");
                return;
            }

            string? selectedEntryType = SimpleFormDialog.ShowComboBoxDialog("Select type.", "Select", entryTypes, ComboBoxStyle.DropDownList);
            if (selectedEntryType == null)
            {
                LogInfo("No entry type selected, cancelling new operation.");
                return;
            }

            string? entryName = SimpleFormDialog.ShowInputDialog("Input name.", "Create");
            if (entryName == null)
            {
                LogInfo("No name given, cancelling new operation.");
                return;
            }

            if (!binding.AddMsbEntry(selectedParamType, selectedEntryType, entryName))
            {
                LogWarn("Failed to add entry, ensure name is unique and correctly formatted.");
                return;
            }

            RepopulateMsbParamView(binding);
            LogInfo($"Successfully added new entry {entryName} to {selectedParamType}.");
        }

        private void DeleteSelectedMsbEntry()
        {
            if (CurrentMsbNodeBinding == null)
            {
                LogWarn($"No MSB is currently selected, cannot delete an entry.");
                return;
            }

            if (Config.ConfirmEntryDelete && !SimpleFormDialog.ShowQuestionDialog("Are you sure you want to delete the select entry?", "Confirm Entry Delete"))
            {
                LogInfo("Cancelled selected entry delete.");
                return;
            }

            if (!GetSelectedMsbEntry(out MsbEntryNode? entry))
            {
                return;
            }

            if (!CurrentMsbNodeBinding.RemoveMsbEntry(entry.Param, entry.Type, entry.Name))
            {
                LogError($"Failed to remove selected entry: {entry.Name}");
                return;
            }

            RepopulateMsbParamView(CurrentMsbNodeBinding);
            LogInfo($"Successfully removed selected entry: {entry.Name}");
        }

        private void DuplicateSelectedMsbEntry()
        {
            if (CurrentMsbNodeBinding == null)
            {
                LogWarn($"No MSB is currently selected, cannot duplicate an entry.");
                return;
            }

            if (!GetSelectedMsbEntry(out MsbEntryNode? entry))
            {
                return;
            }

            if (!CurrentMsbNodeBinding.DuplicateMsbEntry(entry.Param, entry.Type, entry.Name))
            {
                LogError($"Failed to duplicate selected entry: {entry.Name}");
                return;
            }

            RepopulateMsbParamView(CurrentMsbNodeBinding);
            LogInfo($"Successfully duplicated selected entry: {entry.Name}");
        }

        #endregion

        #region Msb Param View Context Menu

        private void NewMsbParamContextMenu_Click(object sender, EventArgs e)
        {
            NewMsbEntry();
        }

        private void DeleteMsbParamContextMenu_Click(object sender, EventArgs e)
        {
            DeleteSelectedMsbEntry();
        }

        private void DuplicateMsbParamContextMenu_Click(object sender, EventArgs e)
        {
            DuplicateSelectedMsbEntry();
        }

        #endregion

        #region IO

        private void PromptOpen()
        {
            string[] files = AcMsbEditor.Windows.Forms.Dialogs.IO.FileDialog.GetFilePaths("Select MSB files to Load", "MapStudio binary files (*.msb)|*.msb|All files (*.*)|*.*");
            OpenFiles(files);
        }

        private void OpenFiles(string[] files)
        {
            foreach (string file in files)
            {
                OpenFile(file);
            }
        }

        private void OpenFile(string file)
        {
            string name = string.Empty;
            if (MsbFileInterface.LoadMsb(file, Config.SelectedMsbGame, out MsbLoadResult result, out MsbFileInfo? msbFileInfo))
            {
                LoadFileView(msbFileInfo);
                name = msbFileInfo.Name;
            }

            switch (result)
            {
                case MsbLoadResult.Success:
                    LogInfo($"Successfully loaded MSB: {name}");
                    break;
                case MsbLoadResult.Exists:
                    LogWarn($"MSB is already loaded: {file}");
                    break;
                case MsbLoadResult.Failure:
                    LogError($"MSB failed to load: {file}");
                    break;
                default:
                    throw new NotSupportedException($"Unknown {nameof(MsbLoadResult)}: {result}");
            }
        }

        private void PromptSave()
        {
            if (FileListView.SelectedItems.Count < 1)
            {
                LogWarn("No MSBs have been selected for save.");
                return;
            }

            if (Config.ConfirmSave && !SimpleFormDialog.ShowQuestionDialog("Are you sure you want to save the selected MSBs?", "Confirm Save"))
            {
                LogInfo("Cancelled save.");
                return;
            }

            SaveFiles(GetSelectedFileInfo());
        }

        private void SaveFiles(List<MsbFileInfo> files)
        {
            foreach (var file in files)
            {
                SaveFile(file);
            }
        }

        private void SaveFile(MsbFileInfo file)
        {
            if (MsbFileInterface.SaveMsb(file.FilePath))
            {
                LogInfo($"Successfully saved MSB: {file.Name}");
            }
            else
            {
                LogError($"MSB failed to save: {file.Name}");
            }
        }

        private void PromptClose()
        {
            if (FileListView.SelectedItems.Count < 1)
            {
                LogWarn("No MSBs have been selected for close.");
                return;
            }

            if (Config.ConfirmClose && !SimpleFormDialog.ShowQuestionDialog("Are you sure you want to close the selected MSBs?", "Confirm Close"))
            {
                LogInfo("Cancelled close.");
                return;
            }

            CloseFiles(CutSelectedFileInfo());
        }

        private void CloseFiles(List<MsbFileInfo> files)
        {
            foreach (var file in files)
            {
                CloseFile(file);
            }
        }

        private void CloseFile(MsbFileInfo file)
        {
            if (MsbFileInterface.CloseMsb(file.FilePath))
            {
                LogInfo($"Successfully closed MSB: {file.Name}");
            }
            else
            {
                LogError($"MSB failed to close: {file.Name}");
            }
        }

        #endregion

        #region Config

        private void SaveConfig()
        {
            if (Config.Modified)
            {
                if (!Config.Save(ConfigPath))
                {
                    LogError($"Failed to save app config to: {ConfigPath}");
                }
                else
                {
                    LogInfo("Saved config.");
                }
            }
        }

        #endregion

        #region Log Context Menu

        private void CopyLogContextMenu_Click(object sender, EventArgs e)
        {
            CopyLog();
        }

        private void ClearLogContextMenu_Click(object sender, EventArgs e)
        {
            LogListBox.Items.Clear();
        }

        #endregion

        #region Log Key Down

        private void LogListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        CopySelectedLog();
                        break;
                }
            }
        }

        #endregion

        #region Log

        private void CopyLog()
        {
            try
            {
                var copyBuffer = new System.Text.StringBuilder();
                foreach (string item in LogListBox.Items)
                {
                    copyBuffer.AppendLine(item);
                }

                if (copyBuffer.Length > 0)
                {
                    Clipboard.SetDataObject(copyBuffer.ToString());
                }
            }
            catch (Exception ex)
            {
                LogError($"Failed to copy log to clipboard: {ex}");
            }
        }

        private void CopySelectedLog()
        {
            try
            {
                var copyBuffer = new System.Text.StringBuilder();
                var selected = LogListBox.SelectedItems;
                foreach (string item in selected)
                {
                    copyBuffer.AppendLine(item);
                }

                if (copyBuffer.Length > 0)
                {
                    Clipboard.SetDataObject(copyBuffer.ToString());
                }
            }
            catch (Exception ex)
            {
                LogError($"Failed to copy selected log to clipboard: {ex}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LogInfo(string message)
            => Log($"Info: {message}");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LogWarn(string message)
            => Log($"Warn: {message}");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LogError(string message)
            => Log($"Error: {message}");

        private void Log(string message)
        {
            if (LogListBox.Items.Count + 1 > Config.MaxLogListBoxEntries)
            {
                LogListBox.Items.RemoveAt(0);
            }

            LogListBox.Items.Add(message);
            LogListBox.ScrollToBottom();
        }

        #endregion

    }
}
