namespace CustomEscapesReworked
{
    using AdminToys;
    using Exiled.API.Enums;
    using Exiled.API.Features.Toys;
    using PlayerRoles;
    using UnityEngine;
    using CustomEscapesReworked.Objects;
    using Exiled.Events.EventArgs.Player;

    public class EventHandler
    {
        internal EventHandler()
        {
            Exiled.Events.Handlers.Server.RoundStarted += SpawnCustomHandlers;
            Exiled.Events.Handlers.Player.Escaping += HandleDefaultEscape;
        }

        ~EventHandler()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= SpawnCustomHandlers;
            Exiled.Events.Handlers.Player.Escaping -= HandleDefaultEscape;
        }

        private void SpawnCustomHandlers()
        {
            foreach (KeyValuePair<Vector3, CustomEscapeHandle[]> handle in Plugin.Instance.Config.CustomPositionScenarios)
            {
                Primitive prim = Primitive.Create(PrimitiveType.Cube,
                    PrimitiveFlags.Collidable,
                    position: handle.Key,
                    isStatic: true);
                
                prim.GameObject.AddComponent<EscapeComponent>().Init(handle.Value);
            }
        }

        private void HandleDefaultEscape(EscapingEventArgs ev)
        {
            if (!Plugin.Instance.Config.DefaultPositionScenarios.TryGetValue(ev.Player.Role.Type,
                    out DefaultEscapeHandle[]? handlers))
                return;

            foreach (DefaultEscapeHandle handle in handlers)
            {
                if (handle.ShouldBeCuffed != ev.Player.IsCuffed)
                    continue;

                ev.EscapeScenario = EscapeScenario.CustomEscape;
                ev.NewRole = handle.NewRole;
                ev.IsAllowed = ev.NewRole != RoleTypeId.None;
                
                if (ev.IsAllowed)
                    handle.EscapeMessage?.ShowMessage(ev.Player);
            }
        }
    }
}