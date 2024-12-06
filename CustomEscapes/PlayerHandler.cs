using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace CustomEscapes;

public class PlayerHandler
{
    private readonly Config config = Escaping.Instance.Config;

    public PlayerHandler()
    {
        Exiled.Events.Handlers.Player.Escaping += OnEscaping;
        Exiled.Events.Handlers.Player.ChangedRole += OnChangedRole;
    }

    ~PlayerHandler()
    {
        Exiled.Events.Handlers.Player.Escaping -= OnEscaping;
        Exiled.Events.Handlers.Player.ChangedRole -= OnChangedRole;
    }

    public void OnEscaping(EscapingEventArgs ev)
    {
        if (!config.EscapeScenarios.TryGetValue(ev.Player.Role.Type, out Escape escapeScenario))
            return;

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
            escapeScenario.CuffedEscapeMessage?.SendMessage(ev.Player);
        else
            escapeScenario.NormalEscapeMessage?.SendMessage(ev.Player);

        Log.Debug($"{ev.Player.Nickname} has escaped as a {ev.Player.Role.Type}! They became {ev.NewRole}");
    }

    public void OnChangedRole(ChangedRoleEventArgs ev)
    {
        if (!config.EscapeScenarios.TryGetValue(ev.Player.Role.Type, out Escape escapeScenario) 
            || (escapeScenario?.NewEscapeNormal?[0] == null 
                && escapeScenario?.NewEscapeCuffed?[0] == null)) return; // if no new escapes for normal nor cuffed, return (nothing to do)
        
        Timing.RunCoroutine(DoCustomEscape(ev.Player, ev.Player.Role.Type, escapeScenario).CancelWith(ev.Player.GameObject));
        Log.Debug("Coroutine for player has run!");
    }

    private IEnumerator<float> DoCustomEscape(Player player, RoleTypeId oldRole, Escape scenario)
    {
        while (player.Role.Type == oldRole)
        {
            if (player.IsCuffed && scenario.NewEscapeCuffed?[0] != null)
            {
                foreach (NewEscapePosition newEscPos in scenario.NewEscapeCuffed)
                {
                    if (Vector3.Distance(player.Position, newEscPos.Position) > newEscPos.Distance)
                        continue;

                    scenario.CuffedEscapeMessage?.SendMessage(player);

                    player.Role.Set(scenario.CuffedRole, SpawnReason.Escaped, RoleSpawnFlags.All);

                    scenario.CuffedTickets?.GrantTickets();
                    break;
                }
            }
            else if (!player.IsCuffed && scenario.NewEscapeNormal?[0] != null)
            {
                foreach (NewEscapePosition newEscPos in scenario.NewEscapeNormal)
                {
                    if (Vector3.Distance(player.Position, newEscPos.Position) > newEscPos.Distance)
                        continue;

                    scenario.NormalEscapeMessage?.SendMessage(player);

                    player.Role.Set(scenario.NormalRole, SpawnReason.Escaped, RoleSpawnFlags.All);

                    scenario.NormalTickets?.GrantTickets();
                    break;
                }
            }
            yield return Timing.WaitForSeconds(1f);
        }
    }
}