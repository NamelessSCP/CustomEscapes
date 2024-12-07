namespace CustomEscapes.Models
{
    using Exiled.API.Features;

    public class EscapeMessage
    {
        public string Message { get; set; }
        public bool UseHints { get; set; }

        public void ShowMessage(Player player)
        {
            if (string.IsNullOrEmpty(Message))
                return;

            if (UseHints)
                player.ShowHint(Message, 5);
            else
                player.Broadcast(5, Message);
        }
    }
}