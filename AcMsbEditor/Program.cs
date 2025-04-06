using ACMsbEditor.Windows.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace ACMsbEditor
{
    internal static class Program
    {
        private const string ConfigName = "config.json";

        private static readonly string AppFolder = AppContext.BaseDirectory;
        private static readonly string ConfigFolder = AppFolder;
        private static readonly string ConfigPath = Path.Combine(ConfigFolder, ConfigName);

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var config = AppConfig.Load(ConfigPath);
            if (config.Outdated)
            {
                config.Update();
                config.Save(ConfigPath);
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(config, ConfigPath));

            if (config.Modified)
            {
                config.Save(ConfigPath);
            }
        }
    }
}