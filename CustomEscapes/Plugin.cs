namespace CustomEscapes;

using Exiled.API.Features;
using Exiled.API.Enums;
using System;

public class Escaping : Plugin<Config>
{
    public override string Name => "CustomEscapes";
    public override string Prefix => "CustomEscapes";
    public override string Author => "@misfiy";
    public override PluginPriority Priority => PluginPriority.Default;
    public override Version Version => new(1, 3, 7);
    public override Version RequiredExiledVersion => new(8, 7, 0);

    private PlayerHandler playerHandler { get; set; } = null!;

    public static Escaping Instance { get; set; } = null!;

    public override void OnEnabled()
    {
        Instance = this;
        playerHandler = new();
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        playerHandler = null!;
        Instance = null!;
        base.OnDisabled();
    }
}