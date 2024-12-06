using PlayerRoles;

namespace CustomEscapesReworked.Objects
{
    public class CustomEscapeHandle
    {
        public RoleTypeId OldRole { get; set; } = RoleTypeId.None;
        public bool ShouldBeCuffed { get; set; }
        public RoleTypeId NewRole { get; set; } = RoleTypeId.None;
    }
}