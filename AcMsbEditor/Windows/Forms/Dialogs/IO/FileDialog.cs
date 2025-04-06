using System.Windows.Forms;

namespace AcMsbEditor.Windows.Forms.Dialogs.IO
{
    internal static class FileDialog
    {
        /// <summary>
        /// Get a single file from a user.
        /// </summary>
        /// <param name="initialDirectory">A string representing the path the dialog box should open in.</param>
        /// <param name="title">A string containing the title to display in the dialog box.</param>
        /// <param name="filter">What filetypes should be shown in the "Files of type" filter.</param>
        /// <returns>A string representing the path to a file a user selects.</returns>
        public static string? GetFilePath(string? initialDirectory = null, string? title = null, string? filter = null)
        {
            OpenFileDialog filePathDialog = new OpenFileDialog()
            {
                InitialDirectory = initialDirectory ?? "C:\\Users",
                Title = title ?? "Select a file to open.",
                Filter = filter ?? "All files (*.*)|*.*"
            };

            return filePathDialog.ShowDialog() == DialogResult.OK ? filePathDialog.FileName : null;
        }

        /// <summary>
        /// Get a save path from a user.
        /// </summary>
        /// <param name="initialDirectory">A string representing the path the dialog box should open in.</param>
        /// <param name="title">A string containing the title to display in the dialog box.</param>
        /// <param name="filter">What filetypes should be shown in the "Save as file type" filter.</param>
        /// <returns>A string representing the save path a user selects.</returns>
        public static string? GetSavePath(string? initialDirectory = null, string? title = null, string? filter = null)
        {
            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                InitialDirectory = initialDirectory ?? "C:\\Users",
                Title = title ?? "Select a location to save to.",
                Filter = filter ?? "All files (*.*)|*.*"
            };

            return saveDialog.ShowDialog() == DialogResult.OK ? saveDialog.FileName : null;
        }

        /// <summary>
        /// Get multiple files from a user.
        /// </summary>
        /// <param name="initialDirectory">A string representing the path the dialog box should open in.</param>
        /// <param name="title">A string containing the title to display in the dialog box.</param>
        /// <param name="filter">What filetypes should be shown in the "Files of type" filter.</param>
        /// <returns>A string array representing the paths to files a user selects.</returns>
        public static string[] GetFilePaths(string? title = null, string? filter = null)
        {
            OpenFileDialog filePathDialog = new OpenFileDialog()
            {
                Title = title ?? "Select a file to open.",
                Filter = filter ?? "All files (*.*)|*.*",
                Multiselect = true,
                RestoreDirectory = true
            };

            return filePathDialog.ShowDialog() == DialogResult.OK ? filePathDialog.FileNames : [];
        }
    }
}
