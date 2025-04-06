using ACMsbEditor.Binding.Msb;
using ACMsbEditor.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ACMsbEditor
{
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        GenerationMode = JsonSourceGenerationMode.Metadata,
        IncludeFields = false,
        ReadCommentHandling = JsonCommentHandling.Skip,
        UseStringEnumConverter = true,
        AllowTrailingCommas = true)]
    [JsonSerializable(typeof(AppConfig))]
    public partial class AppConfigSerializerContext : JsonSerializerContext
    {

    }

    public class AppConfig
    {
        #region Settings

        private string ConfigVersionBacking;
        public string ConfigVersion
        {
            get => ConfigVersionBacking;
            set
            {
                if (value != CurrentConfigVersion)
                {
                    Outdated = true;
                }
                else
                {
                    Outdated = false;
                }

                ConfigVersionBacking = value;
            }
        }

        private MsbGame SelectedMsbGameBacking;
        public MsbGame SelectedMsbGame
        {
            get => SelectedMsbGameBacking;
            set
            {
                if (value != SelectedMsbGameBacking)
                {
                    Modified = true;
                }

                SelectedMsbGameBacking = value;
            }
        }

        private int MaxLogListBoxEntriesBacking;
        public int MaxLogListBoxEntries
        {
            get => MaxLogListBoxEntriesBacking;
            set
            {
                if (value != MaxLogListBoxEntriesBacking)
                {
                    Modified = true;
                }

                MaxLogListBoxEntriesBacking = value;
            }
        }

        private bool ConfirmSaveBacking;
        public bool ConfirmSave
        {
            get => ConfirmSaveBacking;
            set
            {
                if (value != ConfirmSaveBacking)
                {
                    Modified = true;
                }

                ConfirmSaveBacking = value;
            }
        }

        private bool ConfirmCloseBacking;
        public bool ConfirmClose
        {
            get => ConfirmCloseBacking;
            set
            {
                if (value != ConfirmCloseBacking)
                {
                    Modified = true;
                }

                ConfirmCloseBacking = value;
            }
        }

        private bool ConfirmEntryDeleteBacking;
        public bool ConfirmEntryDelete
        {
            get => ConfirmEntryDeleteBacking;
            set
            {
                if (value != ConfirmEntryDeleteBacking)
                {
                    Modified = true;
                }

                ConfirmEntryDeleteBacking = value;
            }
        }

        private bool ShowNameFileListBacking;
        public bool ShowNameFileList
        {
            get => ShowNameFileListBacking;
            set
            {
                if (value != ShowNameFileListBacking)
                {
                    Modified = true;
                }

                ShowNameFileListBacking = value;
            }
        }

        private bool ShowGameFileListBacking;
        public bool ShowGameFileList
        {
            get => ShowGameFileListBacking;
            set
            {
                if (value != ShowGameFileListBacking)
                {
                    Modified = true;
                }

                ShowGameFileListBacking = value;
            }
        }

        private bool ShowVersionFileListBacking;
        public bool ShowVersionFileList
        {
            get => ShowVersionFileListBacking;
            set
            {
                if (value != ShowVersionFileListBacking)
                {
                    Modified = true;
                }

                ShowVersionFileListBacking = value;
            }
        }

        private int WidthFileNameColumnBacking;
        public int WidthFileNameColumn
        {
            get => WidthFileNameColumnBacking;
            set
            {
                if (value != WidthFileNameColumnBacking)
                {
                    Modified = true;
                }

                WidthFileNameColumnBacking = value;
            }
        }

        private int WidthFileGameColumnBacking;
        public int WidthFileGameColumn
        {
            get => WidthFileGameColumnBacking;
            set
            {
                if (value != WidthFileGameColumnBacking)
                {
                    Modified = true;
                }

                WidthFileGameColumnBacking = value;
            }
        }

        private int WidthFileVersionColumnBacking;
        public int WidthFileVersionColumn
        {
            get => WidthFileVersionColumnBacking;
            set
            {
                if (value != WidthFileVersionColumnBacking)
                {
                    Modified = true;
                }

                WidthFileVersionColumnBacking = value;
            }
        }

        #endregion

        #region Book Keeping Properties

        [JsonIgnore]
        private static readonly string CurrentConfigVersion = MakeCurrentConfigVersion();

        [JsonIgnore]
        public bool Modified { get; set; }

        [JsonIgnore]
        public bool Outdated { get; private set; }

        #endregion

        public AppConfig()
        {
            ConfigVersionBacking = "0.0.0.0"; // Shut up linter
            ConfigVersion = ConfigVersionBacking;
            SelectedMsbGame = MsbGame.AC4;
            MaxLogListBoxEntries = 1000;
            ConfirmSave = true;
            ConfirmClose = true;
            ConfirmEntryDelete = true;
            ShowNameFileList = true;
            ShowGameFileList = true;
            ShowVersionFileList = true;
            WidthFileNameColumn = 120;
            WidthFileGameColumn = 60;
            WidthFileVersionColumn = 60;
            Modified = false;
        }

        #region Book Keeping Methods

        private static string MakeCurrentConfigVersion()
        {
            var asmName = Assembly.GetExecutingAssembly().GetName();
            var asmVersion = asmName.Version;
            if (asmVersion == null)
            {
                return "0.0.0.0";
            }

            return asmVersion.ToString();
        }

        public void Update()
        {
            ConfigVersion = CurrentConfigVersion;
        }

        #endregion

        #region IO

        public static AppConfig Load(string path)
        {
            var currentConfig = new AppConfig();

            try
            {
                if (!File.Exists(path))
                {
                    ConsoleLogger.LogWarn($"Config could not be found: {path}");

                    string? directory = Path.GetDirectoryName(path);
                    if (string.IsNullOrEmpty(directory))
                    {
                        ConsoleLogger.LogError($"Unable to save default config, failed to get config folder name from: {path}");
                    }
                    else
                    {
                        ConsoleLogger.LogInfo($"Saving default config to: {path}");
                        Directory.CreateDirectory(directory);
                        if (!currentConfig.Save(path))
                        {
                            ConsoleLogger.LogError($"Failed to save default config to: {path}");
                        }
                    }
                }
                else
                {
                    var config = JsonSerializer.Deserialize(File.ReadAllText(path), AppConfigSerializerContext.Default.AppConfig);
                    if (config == null)
                    {
                        ConsoleLogger.LogError($"Failed to load existing config: {path}");
                    }
                    else
                    {
                        currentConfig = config;
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleLogger.LogError($"An error occurred while loading config: {ex}");
            }

            currentConfig.Modified = false;
            return currentConfig;
        }

        public bool Save(string path)
        {
            try
            {
                string json = JsonSerializer.Serialize(this, AppConfigSerializerContext.Default.AppConfig);
                File.WriteAllText(path, json);
                return true;
            }
            catch (Exception ex)
            {
                ConsoleLogger.LogError($"An error occurred while saving config: {ex}");
                return false;
            }
        }

        #endregion
    }
}
