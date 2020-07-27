namespace MefPlugin.Plugin
{
    public interface IPluginMetadata
    {
        public string Name { get; }
        public int Priority { get; }
    }
}
