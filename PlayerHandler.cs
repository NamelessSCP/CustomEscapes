using Exiled.Events.EventArgs.Player;
using Exiled.API.Enums;
using PlayerRoles;
using Respawning;
using Exiled.API.Features;

namespace CustomEscapes.Events
{
    public sealed class PlayerHandler
    {
        public void OnEscaping(EscapingEventArgs ev)
        {
            Config config = Escaping.Instance.Config;

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
    }
}
