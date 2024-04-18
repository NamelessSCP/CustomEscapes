using Exiled.API.Interfaces;
using PlayerRoles;
using Respawning;
using System.ComponentModel;
using UnityEngine;

namespace CustomEscapes;

public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;
    public Dictionary<RoleTypeId, Escape> EscapeScenarios { get; set; } = new Dictionary<RoleTypeId, Escape>
    {
        {
            RoleTypeId.FacilityGuard, new Escape
            {
                NewEscapeCuffed = new List<NewEscapePosition>
                {
                    new NewEscapePosition {
                        Position = new(-38, 988, -42.5f),
                        Distance = 5f
                    }
                },
                CuffedRole = RoleTypeId.ChaosConscript,
                CuffedTickets = new RespawnTicket
                {
                    Team = SpawnableTeamType.ChaosInsurgency,
                    Number = 4f,
                },
                CuffedEscapeMessage = new EscapeMessage()
                {
                    Message = "You were taken prisoner by the <color=green>Chaos Insurgency!</color>",
                    Duration = 5,
                    UseHints = true,
                },

                NormalRole = RoleTypeId.NtfSpecialist,
                NormalTickets = new RespawnTicket
                {
                    Team = SpawnableTeamType.NineTailedFox,
                    Number = 4f,
                },

            }
        },
        {
            RoleTypeId.ClassD, new Escape
            {
                NewEscapeNormal = new List<NewEscapePosition>
                {
                    new NewEscapePosition {
                        Position = new(-38, 988, -42.5f),
                        Distance = 5f
                    }
                },
                AllowDefaultEscape = false,

                CuffedRole = RoleTypeId.NtfPrivate,
                CuffedTickets = new RespawnTicket
                {
                    Team = SpawnableTeamType.NineTailedFox,
                    Number = 4f,
                },

                NormalRole = RoleTypeId.ChaosConscript,
                NormalTickets = new RespawnTicket
                {
                    Team = SpawnableTeamType.ChaosInsurgency,
                    Number = 4f,
                },
                NormalEscapeMessage = new EscapeMessage()
                {
                    Message = "You leave through the exit with Chaos Insurgency members waiting for you..",
                    Duration = 5,
                    UseHints = true,
                },
            }
        },
    };
}
public class Escape
{
    [Description("The new escape position")]
    public List<NewEscapePosition>? NewEscapeCuffed { get; set; }
    public List<NewEscapePosition>? NewEscapeNormal { get; set; }
    [Description("Whether or not to allow escaping through the default escape (Gate B exit) while uncuffed")]
    public bool AllowDefaultEscape { get; set; } = true;
    public RoleTypeId CuffedRole { get; set; }
    public RespawnTicket? CuffedTickets { get; set; }
    public EscapeMessage? CuffedEscapeMessage { get; set; }
    public RespawnTicket? NormalTickets { get; set; }
    public RoleTypeId NormalRole { get; set; }
    public EscapeMessage? NormalEscapeMessage { get; set; }
}
public class NewEscapePosition
{
    [Description("Vector3 position")]
    public Vector3 Position { get; set; }
    [Description("Distance from the position")]
    public float Distance { get; set; }
}
public class RespawnTicket
{
    public SpawnableTeamType Team { get; set; }
    public float Number { get; set; }
}
public class EscapeMessage
{
    [Description("Message to show to the player when escaping")]
    public string? Message { get; set; }
    public ushort Duration { get; set; }
    [Description("Whether or not to use hints")]
    public bool UseHints { get; set; }
}