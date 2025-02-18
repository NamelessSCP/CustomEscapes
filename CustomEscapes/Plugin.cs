namespace CustomEscapes;

#if EXILED
using Exiled.API.Features;
#else
using LabApi.Loader.Features.Plugins;
#endif

public class Plugin : Plugin<Config>
{
    private EventHandler? _eventHandler;

    public static Plugin Instance { get; private set; } = null!;

    public override string Name => "CustomEscapes";
    public override string Author => "@misfiy";
    public override Version Version => new(2, 0, 0);
#if EXILED
    public override Version RequiredExiledVersion => new(9, 0, 0);
#else
    public override string Description => "Adds Custom escape configs";
    public override Version RequiredApiVersion => new(1, 0, 0);
#endif

#if EXILED
    public override void OnEnabled()
#else
    public override void Enable()
#endif
    {
        Instance = this;
        _eventHandler = new EventHandler();
    }

#if EXILED
    public override void OnDisabled()
#else
    public override void Disable()
#endif
    {
        _eventHandler = null;
        Instance = null!;
    }
}