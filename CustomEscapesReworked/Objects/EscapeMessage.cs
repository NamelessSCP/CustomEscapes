namespace CustomEscapesReworked.Objects
{
    using Exiled.API.Features;

    public class EscapeMessage
    {
        public string Message { get; set; } = string.Empty;
        public bool UseHints { get; set; } = true;

        public void ShowMessage(Player player)
        {
            if (Message == String.Empty)
                return;
            
            if (UseHints)
                player.ShowHint(Message, 5);
            else
                player.Broadcast(5, Message);
        }
    }
}