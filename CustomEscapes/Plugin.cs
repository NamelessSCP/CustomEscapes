namespace CustomEscapes
{
    using Exiled.API.Features;
    using Exiled.API.Enums;
    using CustomEscapes.Events;
    using System;

    public class Escaping : Plugin<Config>
    {
        public override string Name => "CustomEscapes";
        public override string Prefix => "CustomEscapes";
        public override string Author => "@misfiy & Rysik5318";
        public override PluginPriority Priority => PluginPriority.Default;
        public override Version Version => new(1, 3, 3);
        public override Version RequiredExiledVersion => new(8, 2, 1);

        private PlayerHandler playerHandler;
        public static Escaping Instance;
        private Config config;
        public override void OnEnabled()
        {
            Instance = this;
            config = Instance.Config;
            playerHandler = new PlayerHandler();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            playerHandler = null!;
            Instance = null!;
            base.OnDisabled();
        }
    }
}
