namespace CustomEscapes;

using Models;
using Components;
using PlayerRoles;
using UnityEngine;
using LabApi.Events.Arguments.PlayerEvents;

public class EventHandler
{
    internal EventHandler()
    {
        LabApi.Events.Handlers.ServerEvents.RoundStarted += SpawnCustomHandlers;
        LabApi.Events.Handlers.PlayerEvents.Escaping += HandleDefaultEscape;
    }

    ~EventHandler()
    {
        LabApi.Events.Handlers.ServerEvents.RoundStarted -= SpawnCustomHandlers;
        LabApi.Events.Handlers.PlayerEvents.Escaping -= HandleDefaultEscape;
    }

    private void SpawnCustomHandlers()
    {
        foreach (CustomEscapeHandle handle in Plugin.Instance.Config!.CustomEscapeHandles)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.isStatic = true;
            obj.transform.position = handle.Position;
            obj.GetComponent<BoxCollider>().isTrigger = true;
            obj.AddComponent<EscapeComponent>().Init(handle.Handles);
        }
    }

    private void HandleDefaultEscape(PlayerEscapingEventArgs ev)
    {
        foreach (EscapeHandle handle in Plugin.Instance.Config!.EscapeHandles)
        {
            if (handle.OriginalRole != ev.Player.Role || handle.ShouldBeCuffed != ev.Player.IsDisarmed)
                continue;

            ev.EscapeScenario = Escape.EscapeScenarioType.Custom;
            ev.NewRole = handle.NewRole;
            ev.IsAllowed = ev.NewRole != RoleTypeId.None;
                
            if (ev.IsAllowed)
                handle.EscapeMessage?.ShowMessage(ev.Player);
        }
    }
}