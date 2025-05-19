namespace CustomEscapes.Components;

using Models;
using UnityEngine;
using PlayerRoles;
using LabApi.Features.Wrappers;

public class EscapeComponent : MonoBehaviour
{
    private EscapeHandle[] _handlers = null!;

    public void Init(EscapeHandle[] handles)
    {
        _handlers = handles;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!Player.TryGet(collider.gameObject, out Player? player))
            return;

        foreach (EscapeHandle handle in _handlers)
        {
            if (handle.OriginalRole != player.Role)
                continue;

            if (handle.ShouldBeCuffed != player.IsDisarmed)
                continue;

            player.SetRole(handle.NewRole, RoleChangeReason.Escaped);
            handle.EscapeMessage?.ShowMessage(player);
        }
    }
}