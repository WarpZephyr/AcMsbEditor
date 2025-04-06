using System.Windows.Forms;

namespace AcMsbEditor.Windows.Forms.Dialogs.IO
{
    internal static class DirectoryDialog
    {
        /// <summary>
        /// Get a single folder from a user.
        /// </summary>
        /// <param name="initialDirectory">A string representing the path the dialog box should open in.</param>
        /// <param name="title">A string containing the title to display in the dialog box.</param>
        /// <returns>A string representing the path to a folder a user selects.</returns>
        public static string? GetFolderPath(string? initialDirectory = null, string? title = null)
        {
            FolderBrowserDialog filePathDialog = new FolderBrowserDialog()
            {
                InitialDirectory = initialDirectory ?? "C:\\Users",
                Description = title ?? "Select a folder to open.",
                UseDescriptionForTitle = true
            };

            return filePathDialog.ShowDialog() == DialogResult.OK ? filePathDialog.SelectedPath : null;
        }
    }
}
