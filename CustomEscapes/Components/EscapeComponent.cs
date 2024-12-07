namespace CustomEscapes.Components
{
    using CustomEscapes.Models;
    using Exiled.API.Features;
    using Exiled.API.Enums;
    using UnityEngine;

    public class EscapeComponent : MonoBehaviour
    {
        private EscapeHandle[] handlers = null!;

        public void Init(EscapeHandle[] handles)
        {
            handlers = handles;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!Player.TryGet(collider, out Player player))
                return;

            foreach (EscapeHandle handle in handlers)
            {
                if (handle.OriginalRole != player.Role.Type)
                    continue;

                if (handle.ShouldBeCuffed != player.IsCuffed)
                    continue;

                player.Role.Set(handle.NewRole, SpawnReason.Escaped);
                handle.EscapeMessage?.ShowMessage(player);
            }
        }
    }
}