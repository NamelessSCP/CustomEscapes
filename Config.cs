using System;
using System.Collections.Generic;
using Exiled.API.Interfaces;
using Exiled.API.Enums;
using PlayerRoles;
using YamlDotNet.Serialization;
using Respawning;
using System.ComponentModel;
using UnityEngine;

namespace CustomEscapes
{
    public sealed class Config : IConfig
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
        public RespawnTicket CuffedTickets { get; set; }
        public RespawnTicket NormalTickets { get; set; }
        public RoleTypeId NormalRole { get; set; }
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
}
