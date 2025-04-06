using System.IO;

namespace AcMsbEditor.IO
{
    internal static class FileHelper
    {
        internal static void Backup(string path)
        {
            if (File.Exists(path))
            {
                string backupPath = path + ".bak";
                if (!File.Exists(backupPath))
                {
                    File.Move(path, backupPath);
                }
            }
        }
    }
}
