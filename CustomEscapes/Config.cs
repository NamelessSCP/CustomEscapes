namespace CustomEscapes;

#if EXILED
using Exiled.API.Interfaces;
#endif

using Models;
using PlayerRoles;
using UnityEngine;

#if EXILED
public class Config : IConfig
#else
public class Config
#endif
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