namespace CustomEscapesReworked
{
    using CustomEscapesReworked.Objects;
    using Exiled.API.Interfaces;
    using PlayerRoles;
    using UnityEngine;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public Dictionary<Vector3, CustomEscapeHandle[]> CustomPositionScenarios { get; set; } = new()
        {
            {
                Vector3.one,
                new CustomEscapeHandle[]
                {
                    new()
                    {
                        OldRole = RoleTypeId.FacilityGuard,
                        ShouldBeCuffed = false,
                        NewRole = RoleTypeId.NtfSpecialist,
                    }
                }
            }
        };

        public Dictionary<RoleTypeId, DefaultEscapeHandle[]> DefaultPositionScenarios { get; set; } = new()
        {
            {
                RoleTypeId.FacilityGuard,
                new DefaultEscapeHandle[]
                {
                    new()
                    {
                        ShouldBeCuffed = false,
                        NewRole = RoleTypeId.NtfSpecialist,
                    },
                    new()
                    {
                        ShouldBeCuffed = true,
                        NewRole = RoleTypeId.ChaosConscript,
                    },
                }
            }
        };
    }
}