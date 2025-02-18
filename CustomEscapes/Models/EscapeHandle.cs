namespace CustomEscapes.Models;

using PlayerRoles;

public class EscapeHandle
{
    public bool ShouldBeCuffed { get; set; }
    public RoleTypeId OriginalRole { get; set; }
    public RoleTypeId NewRole { get; set; }
    public EscapeMessage? EscapeMessage { get; set; }
}