using Exiled.API.Features;
using Exiled.API.Enums;

namespace CustomEscapesSimplifed;

public class Plugin : Plugin<Config>
{
    public static Plugin Instance { get; private set; } = null!;

    public override string Name { get; } = "CustomEscapes";
    public override string Author { get; } = "@misfiy";
    public override PluginPriority Priority => PluginPriority.Default;
    public override Version Version => new(1, 3, 7);
    public override Version RequiredExiledVersion => new(8, 7, 0);

    private EventHandler _eventHandler { get; set; } = null!;

    public override void OnEnabled()
    {
        Instance = this;
        _eventHandler = new();

        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        _eventHandler = null!;
        Instance = null!;

        base.OnDisabled();
    }
}