using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using Respawning;
using UnityEngine;

namespace CustomEscapes
{
    public sealed class PlayerHandler
    {
        private readonly Config config = Escaping.Instance.Config;

        public PlayerHandler()
        {
            Exiled.Events.Handlers.Player.Escaping += OnEscaping;
            Exiled.Events.Handlers.Player.Spawned += OnSpawned;
        }

        ~PlayerHandler()
        {
            Exiled.Events.Handlers.Player.Escaping -= OnEscaping;
            Exiled.Events.Handlers.Player.Spawned -= OnSpawned;
        }


        public void OnEscaping(EscapingEventArgs ev)
        {
            if (!config.EscapeScenarios.TryGetValue(ev.Player.Role.Type, out Escape escapeScenario)) return;

            if (!escapeScenario.AllowDefaultEscape && !ev.Player.IsCuffed)
            {
                ev.IsAllowed = false;
                return;
            }

            ev.NewRole = ev.Player.IsCuffed ? escapeScenario.CuffedRole : escapeScenario.NormalRole;

            ev.RespawnTickets = ev.Player.IsCuffed 
                ? escapeScenario.CuffedTickets != null 
                    ? new(escapeScenario.CuffedTickets.Team, escapeScenario.CuffedTickets.Number) 
                    : ev.RespawnTickets 
                : escapeScenario.NormalTickets != null 
                    ? new(escapeScenario.NormalTickets.Team, escapeScenario.NormalTickets.Number) 
                    : ev.RespawnTickets;

            if (ev.NewRole == RoleTypeId.None)
            {
                Log.Debug($"{ev.Player.Nickname} attempted to escape, but NewRole is set to None!");
                ev.IsAllowed = false;
                return;
            }

            ev.EscapeScenario = EscapeScenario.CustomEscape;
            ev.IsAllowed = true;
            if (ev.Player.IsCuffed)
            {
                if (escapeScenario.CuffedEscapeMessage != null)
                {
                    if (escapeScenario.CuffedEscapeMessage.UseHints) ev.Player.ShowHint(escapeScenario.CuffedEscapeMessage.Message, escapeScenario.CuffedEscapeMessage.Duration);
                    else ev.Player.Broadcast(escapeScenario.CuffedEscapeMessage.Duration, escapeScenario.CuffedEscapeMessage.Message);
                }
            }
            else
            {
                if (escapeScenario.NormalEscapeMessage != null)
                {
                    if (escapeScenario.NormalEscapeMessage.UseHints) ev.Player.ShowHint(escapeScenario.NormalEscapeMessage.Message, escapeScenario.NormalEscapeMessage.Duration);
                    else ev.Player.Broadcast(escapeScenario.NormalEscapeMessage.Duration, escapeScenario.NormalEscapeMessage.Message);
                }
            }
            Log.Debug($"{ev.Player.Nickname} has escaped as a {ev.Player.Role.Type}! They became {ev.NewRole}");

        }
        public void OnSpawned(SpawnedEventArgs ev)
        {
            if (!config.EscapeScenarios.TryGetValue(ev.Player.Role.Type, out Escape escapeScenario) 
                || (escapeScenario?.NewEscapeNormal?[0] == null 
                && escapeScenario?.NewEscapeCuffed?[0] == null)) return; // if no new escapes for normal nor cuffed, return (nothing to do)
            Timing.RunCoroutine(DoCustomEscape(ev.Player, ev.Player.Role.Type, escapeScenario).CancelWith(ev.Player.GameObject));
            Log.Info("Coroutine for player has run!");
        }
        private IEnumerator<float> DoCustomEscape(Player player, RoleTypeId oldRole, Escape scenario)
        {
            while (player.Role.Type == oldRole)
            {
                if (player.IsCuffed && scenario.NewEscapeCuffed?[0] != null)
                {
                    foreach (NewEscapePosition newEscPos in scenario.NewEscapeCuffed)
                    {
                        if (Vector3.Distance(player.Position, newEscPos.Position) <= newEscPos.Distance)
                        {
                            if (scenario.CuffedEscapeMessage != null)
                            {
                                if (scenario.CuffedEscapeMessage.UseHints)
                                    player.ShowHint(scenario.CuffedEscapeMessage.Message, scenario.CuffedEscapeMessage.Duration);
                                else
                                    player.Broadcast(scenario.CuffedEscapeMessage.Duration, scenario.CuffedEscapeMessage.Message);
                            }
                            player.Role.Set(scenario.CuffedRole, SpawnReason.Escaped, RoleSpawnFlags.All);
                            if (scenario.CuffedTickets != null)
                                Respawn.GrantTickets(scenario.CuffedTickets.Team, scenario.CuffedTickets.Number);
                        }
                    }
                }
                else if (!player.IsCuffed && scenario.NewEscapeNormal?[0] != null)
                {
                    foreach (NewEscapePosition newEscPos in scenario.NewEscapeNormal)
                    {
                        if (Vector3.Distance(player.Position, newEscPos.Position) <= newEscPos.Distance)
                        {
                            if (scenario.NormalEscapeMessage != null)
                            {
                                if (scenario.NormalEscapeMessage.UseHints)
                                    player.ShowHint(scenario.NormalEscapeMessage.Message, scenario.NormalEscapeMessage.Duration);
                                else
                                    player.Broadcast(scenario.NormalEscapeMessage.Duration, scenario.NormalEscapeMessage.Message);
                            }
                            player.Role.Set(scenario.NormalRole, SpawnReason.Escaped, RoleSpawnFlags.All);
                            if (scenario.NormalTickets != null)
                                Respawn.GrantTickets(scenario.NormalTickets.Team, scenario.NormalTickets.Number);
                        }
                    }
                }
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }

}
