namespace CustomEscapes;

using Exiled.API.Features;
using Exiled.API.Enums;
using System;

public class Escaping : Plugin<Config>
{
    public static Escaping Instance { get; set; } = null!;

    public override string Name { get; } = "CustomEscapes";
    public override string Prefix => Name;
    public override string Author { get; } = "@misfiy";
    public override PluginPriority Priority => PluginPriority.Default;
    public override Version Version => new(1, 3, 7);
    public override Version RequiredExiledVersion => new(8, 7, 0);

    private PlayerHandler playerHandler { get; set; } = null!;

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