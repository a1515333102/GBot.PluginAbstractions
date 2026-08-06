namespace GBot.PluginAbstractions;

public sealed class PluginInfo
{
    public required string Name { get; init; }
    public required string Version { get; init; }
    public required string Author { get; init; }
    public string Description { get; init; } = "";
    public string Homepage { get; init; } = "";
    public string Id { get; init; } = "";
}
