using AcMsbEditor.Binding.Nodes;
using SoulsFormats;

namespace ACMsbEditor.Binding.Msb
{
    public class Ac4MsbFile : IMsbFile
    {
        public string FilePath { get; set; }
        public string Name { get; set; }
        public MsbGame Game => MsbGame.AC4;
        public MSBAC4 Msb { get; set; }
        private readonly Ac4MsbNodeBinding Ac4MsbNodeBinding;
        public IMsbNodeBinding MsbNodeBinding => Ac4MsbNodeBinding;

        public Ac4MsbFile(string path, string name, MSBAC4 msb)
        {
            FilePath = path;
            Name = name;
            Msb = msb;
            Ac4MsbNodeBinding = new Ac4MsbNodeBinding(msb);
        }

        public int GetVersion()
            => Msb.Models.Version;

        public void Save()
        {
            Msb.Write(FilePath);
        }
    }
}
