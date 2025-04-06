using SoulsFormats;
using System.Collections.Generic;

namespace AcMsbEditor.Binding.Nodes
{
    public record MsbParamNode
    {
        public required string Name { get; init; }
        public required List<MsbEntryNode> Entries { get; init; }
        public static MsbParamNode Create(string name)
        {
            var paramNode = new MsbParamNode()
            {
                Name = name,
                Entries = []
            };

            return paramNode;
        }

        public void AddEntries<T>(List<T> entries, string type) where T : IMsbEntry
        {
            foreach (var entry in entries)
            {
                Entries.Add(MsbEntryNode.Create(entry, Name, type));
            }
        }
    }
}
