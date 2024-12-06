namespace CustomEscapes.Objects
{
    using PlayerRoles;

    public class DefaultEscapeHandle
    {
        public bool ShouldBeCuffed { get; set; }
        public RoleTypeId NewRole { get; set; } = RoleTypeId.None;
        public EscapeMessage? EscapeMessage { get; set; } = new();
    }
}