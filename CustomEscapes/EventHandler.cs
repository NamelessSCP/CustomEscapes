namespace CustomEscapes
{
    using Exiled.API.Enums;
    using PlayerRoles;
    using UnityEngine;
    using CustomEscapes.Objects;
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
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.isStatic = true;
                obj.transform.position = handle.Key;
                obj.GetComponent<BoxCollider>().isTrigger = true;
                obj.AddComponent<EscapeComponent>().Init(handle.Value);
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