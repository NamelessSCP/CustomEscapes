using PlayerRoles;

namespace CustomEscapesSimplifed.Objects;

public class CustomEscapeScenario
{
    public RoleTypeId NormalRole { get; set; }
    public RespawnTicket NormalTickets { get; set; } = new();

    public RoleTypeId CuffedRole { get; set; }
    public RespawnTicket CuffedTickets { get; set; } = new();
}