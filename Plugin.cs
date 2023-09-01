namespace CustomEscapes
{
     using Exiled.API.Features;
     using Exiled.API.Enums;
     using CustomEscapes.Events;
     using Player = Exiled.Events.Handlers.Player;

     public class Escaping : Plugin<Config>
     {
          public override string Name => "CustomEscapes";
          public override string Prefix => "CustomEscapes";
          public override string Author => "@misfiy";
          public override PluginPriority Priority => PluginPriority.Default;
          private PlayerHandler playerHandler;
          public static Escaping Instance;
          private Config config;
          public override void OnEnabled()
          {
               Instance = this;
               config = Instance.Config;

               RegisterEvents();
               base.OnEnabled();
          }

          public override void OnDisabled()
          {
               UnregisterEvents();
               Instance = null!;
               base.OnDisabled();
          }
          public void RegisterEvents()
          {
               playerHandler = new PlayerHandler();
               Player.Escaping += playerHandler.OnEscaping;
               Player.Spawned += playerHandler.OnSpawned;

               Log.Debug("Events have been registered!");
          }
          public void UnregisterEvents()
          {
               Player.Escaping -= playerHandler.OnEscaping;
               Player.Spawned -= playerHandler.OnSpawned;

               playerHandler = null!;
          }
     }
}