using Exiled.API.Interfaces;
using PlayerRoles;
using CustomEscapesSimplifed.Objects;
using Respawning;

namespace CustomEscapesSimplifed;

public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;

    public Dictionary<RoleTypeId, CustomEscapeScenario> CustomEscapes { get; set; } = new()
    {
        {
            RoleTypeId.FacilityGuard,
            new()
            {
                NormalRole = RoleTypeId.None,
                CuffedRole = RoleTypeId.ChaosConscript,
                CuffedTickets = new()
                {
                    Team = SpawnableTeamType.ChaosInsurgency,
                    Amount = 1,
                }
            }
        }
    };
}