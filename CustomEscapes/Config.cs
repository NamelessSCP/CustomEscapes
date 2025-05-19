namespace CustomEscapes;

using Models;
using PlayerRoles;
using UnityEngine;

public class Config
{
    public EscapeHandle[] EscapeHandles { get; set; } = new EscapeHandle[]
    {
        new()
        {
            OriginalRole = RoleTypeId.FacilityGuard,
            NewRole = RoleTypeId.NtfSpecialist,
            ShouldBeCuffed = false,
            EscapeMessage = new EscapeMessage(message: "You escaped!", useHints: true)
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
                    EscapeMessage = new EscapeMessage(message: "You escaped!", useHints: true)
                }
            }
        }
    };
}