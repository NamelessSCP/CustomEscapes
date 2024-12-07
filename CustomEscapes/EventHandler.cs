namespace CustomEscapes
{
    using CustomEscapes.Models;
    using CustomEscapes.Components;
    using Exiled.API.Enums;
    using PlayerRoles;
    using UnityEngine;
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
            foreach (CustomEscapeHandle handle in Plugin.Instance.Config.CustomEscapeHandles)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.isStatic = true;
                obj.transform.position = handle.Position;
                obj.GetComponent<BoxCollider>().isTrigger = true;
                obj.AddComponent<EscapeComponent>().Init(handle.Handles);
            }
        }

        private void HandleDefaultEscape(EscapingEventArgs ev)
        {
            foreach (EscapeHandle handle in Plugin.Instance.Config.EscapeHandles)
            {
                if (handle.OriginalRole != ev.Player.Role.Type || handle.ShouldBeCuffed != ev.Player.IsCuffed)
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