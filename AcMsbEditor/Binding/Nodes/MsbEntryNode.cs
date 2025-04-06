using SoulsFormats;

namespace AcMsbEditor.Binding.Nodes
{
    public record MsbEntryNode
    {
        public required string Name { get; init; }
        public required string Param { get; init; }
        public required string Type { get; init; }
        public static MsbEntryNode Create(IMsbEntry entry, string param, string type)
        {
            return new MsbEntryNode()
            {
                Name = entry.Name,
                Param = param,
                Type = type
            };
        }
    }
}
