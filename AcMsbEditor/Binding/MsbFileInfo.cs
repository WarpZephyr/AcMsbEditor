using ACMsbEditor.Binding.Msb;

namespace ACMsbEditor.Binding
{
    public record MsbFileInfo
    {
        public required string Name { get; init; }
        public required string FilePath { get; init; }
        public required MsbGame Game { get; init; }
        public required int Version { get; init; }

        public static MsbFileInfo Create(IMsbFile msbFile)
        {
            return new MsbFileInfo()
            {
                Name = msbFile.Name,
                FilePath = msbFile.FilePath,
                Game = msbFile.Game,
                Version = msbFile.GetVersion()
            };
        }
    }
}
