namespace CustomEscapes.Models;

using LabApi.Features.Wrappers;

public class EscapeMessage
{
    public EscapeMessage()
    {
    }

    public EscapeMessage(string message, bool useHints)
    {
        Message = message;
        UseHints = useHints;
    }

    public string Message { get; set; } = string.Empty;
    public bool UseHints { get; set; }

    public void ShowMessage(Player player)
    {
        if (string.IsNullOrEmpty(Message))
            return;

        if (UseHints)
            player.SendHint(Message, 5);
        else
            player.SendBroadcast(Message, 5);
    }
}