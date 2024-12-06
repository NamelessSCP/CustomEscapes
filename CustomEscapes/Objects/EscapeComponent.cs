namespace CustomEscapes.Objects
{
    using Exiled.API.Features;
    using Exiled.API.Enums;
    using UnityEngine;

    public class EscapeComponent : MonoBehaviour
    {
        private CustomEscapeHandle[] handlers = null!;

        public void Init(CustomEscapeHandle[] handles)
        {
            handlers = handles;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!Player.TryGet(other, out Player player))
                return;

            foreach (CustomEscapeHandle handle in handlers)
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