using ACMsbEditor.Binding.Msb;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AcMsbEditor.Binding.Nodes
{
    public interface IMsbNodeBinding
    {
        public MsbGame Game { get; }

        public bool AddMsbEntry(string paramType, string entryType, string entryName);
        public bool RemoveMsbEntry(string paramType, string entryType, string entryName);
        public bool DuplicateMsbEntry(string paramType, string entryType, string entryName);

        public IEnumerable<MsbParamNode> GetMsbParams();
        public bool GetMsbEntryTypes(string paramType, [NotNullWhen(true)] out string[]? types);
        public string[] GetMsbParamTypes();
    }
}
