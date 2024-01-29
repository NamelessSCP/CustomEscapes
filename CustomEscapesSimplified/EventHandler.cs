using CustomEscapesSimplifed.Objects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace CustomEscapesSimplifed;

public class EventHandler
{
    public EventHandler()
    {
        Exiled.Events.Handlers.Player.Escaping += OnEscaping;
    }

    ~EventHandler()
    {
        Exiled.Events.Handlers.Player.Escaping -= OnEscaping;
    }

    private void OnEscaping(EscapingEventArgs ev)
    {
        if (!Plugin.Instance.Config.CustomEscapes.TryGetValue(ev.Player.Role.Type, out CustomEscapeScenario scenario))
            return;
        
        Log.Debug("Loading Escape for: " + ev.Player.Role.Type);
        
        ev.NewRole = ev.Player.IsCuffed ? scenario.CuffedRole : scenario.NormalRole;
        
        ev.RespawnTickets = ev.Player.IsCuffed 
            ? scenario.CuffedTickets != null 
                ? new(scenario.CuffedTickets.Team, scenario.CuffedTickets.Amount) 
                : ev.RespawnTickets 
            : scenario.NormalTickets != null 
                ? new(scenario.NormalTickets.Team, scenario.NormalTickets.Amount) 
                : ev.RespawnTickets;
        
        if (ev.NewRole == RoleTypeId.None)
        {
            Log.Debug($"{ev.Player.Nickname} attempted to escape, but NewRole is set to None!");
            ev.IsAllowed = false;
            return;
        }
        
        ev.EscapeScenario = EscapeScenario.CustomEscape;
        ev.IsAllowed = true;
        
        Log.Debug($"{ev.Player.Nickname} has escaped as a {ev.Player.Role.Type}! They became {ev.NewRole}");
    }
}