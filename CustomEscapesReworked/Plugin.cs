namespace CustomEscapesReworked
{
    using Exiled.API.Features;

    public class Plugin : Plugin<Config>
    {
        private EventHandler? eventHandler;

        public static Plugin Instance { get; private set; } = null!;

        public override string Name { get; } = "CustomEscapes";
        public override string Author { get; } = "@misfiy";
        public override Version Version { get; } = new(2, 0, 0);
        public override Version RequiredExiledVersion { get; } = new(9, 0, 0);

        public override void OnEnabled()
        {
            Instance = this;
            eventHandler = new();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            eventHandler = null;
            Instance = null!;
            
            base.OnDisabled();
        }
    }
}