using Exiled.Events.EventArgs.Player;
using Exiled.API.Enums;
using PlayerRoles;
using Respawning;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace CustomEscapes.Events
{
    public sealed class PlayerHandler
    {
        Config config = Escaping.Instance.Config;
        public void OnEscaping(EscapingEventArgs ev)
        {
            if (config.EscapeScenarios.TryGetValue(ev.Player.Role.Type, out Escape escapeScenario))
            {
                if (ev.Player.IsCuffed)
                {
                    ev.NewRole = escapeScenario.CuffedRole;
                    // ev.RespawnTickets = escapeScenario.CuffedTickets;
                }
                else
                {
                    ev.NewRole = escapeScenario.NormalRole;
                    // ev.RespawnTickets = escapeScenario.NormalTickets;
                }

                if (ev.NewRole == RoleTypeId.None)
                {
                    Log.Debug($"{ev.Player.Nickname} attempted to escape, but NewRole is set to None!");
                    ev.IsAllowed = false;
                    return;
                }

                ev.EscapeScenario = EscapeScenario.CustomEscape;
                ev.IsAllowed = true;

                Log.Debug($"{ev.Player.Nickname} has escaped as a {ev.Player.Role.Type.ToString()}! They became {ev.NewRole.ToString()}");
            }
        }
        public void OnSpawned(SpawnedEventArgs ev)
        {
            // bool flag = false;
            if (!config.EscapeScenarios.TryGetValue(ev.Player.Role.Type, out Escape escapeScenario)) return;
            if (!escapeScenario.AllowNewEscape) return;
            Timing.RunCoroutine(DoCustomEscape(ev.Player, ev.Player.Role.Type, escapeScenario).CancelWith(ev.Player.GameObject));
        }
        private IEnumerator<float> DoCustomEscape(Player player, RoleTypeId oldRole, Escape scenario)
        {
            Vector3 escapePosition = new(-38, 988, -42.5f);
            while (player.Role.Type == oldRole)
            {
                // yield return Timing.WaitForSeconds(1f);
                if(Vector3.Distance(player.Position, escapePosition) >= 5f) yield return Timing.WaitForSeconds(1f);
                else
                {
                    if(player.IsCuffed && scenario.CuffedRole != RoleTypeId.None)
                    {
                        player.Role.Set(scenario.CuffedRole, SpawnReason.Escaped, RoleSpawnFlags.All);
                    }
                    if(!player.IsCuffed && scenario.NormalRole != RoleTypeId.None)
                    {
                        player.Role.Set(scenario.NormalRole, SpawnReason.Escaped, RoleSpawnFlags.All);
                    }
                }
            }
            // yield break;
        }
    }

}