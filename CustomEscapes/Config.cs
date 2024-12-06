namespace CustomEscapes
{
    using System.ComponentModel;

    using CustomEscapes.Objects;
    using Exiled.API.Interfaces;
    using PlayerRoles;
    using UnityEngine;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Yes I know there's a ? there. I don't know why. Removing it breaks everything. Keep it.")]
        public Dictionary<Vector3, CustomEscapeHandle[]> CustomPositionScenarios { get; set; } = new()
        {
            {
                Vector3.one,
                new CustomEscapeHandle[]
                {
                    new()
                    {
                        OriginalRole = RoleTypeId.FacilityGuard,
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