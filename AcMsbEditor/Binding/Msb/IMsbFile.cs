using AcMsbEditor.Binding.Nodes;

namespace ACMsbEditor.Binding.Msb
{
    public interface IMsbFile
    {
        public string FilePath { get; }
        public string Name { get; }
        public MsbGame Game { get; }
        public IMsbNodeBinding MsbNodeBinding { get; }

        public int GetVersion();
        public void Save();
    }
}
