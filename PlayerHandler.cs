using Exiled.Events.EventArgs.Player;
using Exiled.API.Enums;
using PlayerRoles;
using Respawning;
using Exiled.API.Features;
using MEC;
using UnityEngine;
using System.Collections.Generic;

namespace CustomEscapes.Events
{
    public sealed class PlayerHandler
    {
        Config config = Escaping.Instance.Config;
        public void OnEscaping(EscapingEventArgs ev)
        {
            if (config.EscapeScenarios.TryGetValue(ev.Player.Role.Type, out Escape escapeScenario))
            {
                if (!escapeScenario.AllowDefaultEscape && ev.Player.IsCuffed == false)
                {
                    ev.IsAllowed = false;
                    return;
                }

                if (ev.Player.IsCuffed)
                {
                    ev.NewRole = escapeScenario.CuffedRole;
                    GrantOrRemoveTokens(escapeScenario.CuffedTickets.Team, escapeScenario.CuffedTickets.Number - ev.RespawnTickets.Value);
                }   
                else
                {
                    ev.NewRole = escapeScenario.NormalRole;
                    GrantOrRemoveTokens(escapeScenario.NormalTickets.Team, escapeScenario.NormalTickets.Number - ev.RespawnTickets.Value);
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
            if ((escapeScenario?.NewEscapeNormal?[0] == null) && (escapeScenario?.NewEscapeCuffed?[0] == null)) return; // if no new escapes for normal nor cuffed, return (nothing to do)
            Timing.RunCoroutine(DoCustomEscape(ev.Player, ev.Player.Role.Type, escapeScenario).CancelWith(ev.Player.GameObject));
        }
        private IEnumerator<float> DoCustomEscape(Player player, RoleTypeId oldRole, Escape scenario)
        {
            while (player.Role.Type == oldRole)
            {
                // yield return Timing.WaitForSeconds(1f);
                if (player.IsCuffed && scenario.NewEscapeCuffed?[0] != null)
                {
                    foreach (NewEscapePosition newEscPos in scenario.NewEscapeCuffed)
                    {
                        if (Vector3.Distance(player.Position, newEscPos.Position) <= newEscPos.Distance)
                        {
                            player.Role.Set(scenario.CuffedRole, SpawnReason.Escaped, RoleSpawnFlags.All);
                            GrantOrRemoveTokens(scenario.CuffedTickets.Team, scenario.CuffedTickets.Number);
                        }

                    }
                } 
                else if (!player.IsCuffed && scenario.NewEscapeNormal?[0] != null)
                {
                    foreach (NewEscapePosition newEscPos in scenario.NewEscapeNormal)
                    {
                        if (Vector3.Distance(player.Position, newEscPos.Position) <= newEscPos.Distance)
                        {
                            player.Role.Set(scenario.NormalRole, SpawnReason.Escaped, RoleSpawnFlags.All);
                            GrantOrRemoveTokens(scenario.NormalTickets.Team, scenario.NormalTickets.Number);
                        }

                    }
                } 
                else
                {
                    yield break; // something horrific has happened, stop whatever we're doing
                }
                yield return Timing.WaitForSeconds(1f);
            }
            // yield break;
        }
        private void GrantOrRemoveTokens(SpawnableTeamType team, float tokens) {
            if (tokens >= 0)
            {
                Respawn.GrantTickets(team, tokens);
            } 
            else
            {
                Respawn.RemoveTickets(team, Math.Abs(tokens));
            }
        }

    }

}
