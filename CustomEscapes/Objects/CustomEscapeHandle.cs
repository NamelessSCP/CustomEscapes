namespace CustomEscapes.Objects
{
    using PlayerRoles;

    public class CustomEscapeHandle : DefaultEscapeHandle
    {
        public RoleTypeId OriginalRole { get; set; } = RoleTypeId.None;
    }
}