namespace CustomEscapes
{
    using CustomEscapes.Models;
    using Exiled.API.Interfaces;
    using PlayerRoles;
    using UnityEngine;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public EscapeHandle[] EscapeHandles { get; set; } = new EscapeHandle[]
        {
            new()
            {
                OriginalRole = RoleTypeId.FacilityGuard,
                NewRole = RoleTypeId.NtfSpecialist,
                ShouldBeCuffed = false,
                EscapeMessage = new()
                {
                    Message = "You escaped!",
                    UseHints = true,
                }
            }
        };

        public CustomEscapeHandle[] CustomEscapeHandles { get; set; } = new CustomEscapeHandle[]
        {
            new()
            {
                Position = Vector3.zero,
                Handles = new EscapeHandle[]
                {
                    new()
                    {
                        OriginalRole = RoleTypeId.FacilityGuard,
                        NewRole = RoleTypeId.NtfSpecialist,
                        ShouldBeCuffed = false,
                        EscapeMessage = new()
                        {
                            Message = "You escaped!",
                            UseHints = true,
                        }
                    }
                }
            }
        };
    }
}