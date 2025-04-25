namespace CustomEscapes;

using LabApi.Loader.Features.Plugins;

public class Plugin : Plugin<Config>
{
    private EventHandler? _eventHandler;

    public static Plugin Instance { get; private set; } = null!;

    public override string Name => "CustomEscapes";

    public override string Author => "@misfiy";

    public override Version Version => new(3, 0, 0);

    public override string Description => "Adds Custom escape configs";

    public override Version RequiredApiVersion => new(1, 0, 0);

    public override void Enable()
    {
        Instance = this;
        _eventHandler = new EventHandler();
    }

    public override void Disable()
    {
        _eventHandler = null;
        Instance = null!;
    }
}