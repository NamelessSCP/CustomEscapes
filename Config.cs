using System;
using System.Collections.Generic;
using Exiled.API.Interfaces;
using Exiled.API.Enums;
using PlayerRoles;
using YamlDotNet.Serialization;
using Respawning;

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
                    AllowNewEscape = false,
                    CuffedRole = RoleTypeId.ChaosConscript,
                    // CuffedTickets = new KeyValuePair<SpawnableTeamType, float>(SpawnableTeamType.ChaosInsurgency, 1),
                    NormalRole = RoleTypeId.NtfSpecialist,
                    // NormalTickets = new KeyValuePair<SpawnableTeamType, float>(SpawnableTeamType.NineTailedFox, 1),
               }
          }
        };
    }
    public class Escape
    {
        public bool AllowNewEscape { get; set; }
        public RoleTypeId CuffedRole { get; set; }
        // public KeyValuePair<SpawnableTeamType, float> CuffedTickets { get; set; }
        public RoleTypeId NormalRole { get; set; }
        // public KeyValuePair<SpawnableTeamType, float> NormalTickets { get; set; }
    }
}