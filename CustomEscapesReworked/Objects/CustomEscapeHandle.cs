namespace CustomEscapesReworked.Objects
{
    using PlayerRoles;

    public class CustomEscapeHandle : DefaultEscapeHandle
    {
        public RoleTypeId OldRole { get; set; } = RoleTypeId.None;
    }
}