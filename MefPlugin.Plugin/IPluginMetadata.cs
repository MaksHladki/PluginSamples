namespace MefPlugin.Plugin
{
    public interface IPluginMetadata
    {
        string Name { get; }
        int Priority { get; }
    }
}
