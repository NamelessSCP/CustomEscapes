using System.ComponentModel;
using Exiled.API.Features;

namespace CustomEscapesSimplifed.Objects;

public class EscapeMessage
{
     [Description("Message to show to the player when escaping")]
     public string Message { get; set; } = string.Empty;
     [Description("The duration of the message")]
     public ushort Duration { get; set; } = 3;
     [Description("Whether or not to use hints instead")]
     public bool UseHints { get; set; } = true;

     public void Show(Player player)
     {
          if (Message.IsEmpty()) return;
          
          if (UseHints)
               player.ShowHint(Message, Duration);
          else
               player.Broadcast(Duration, Message);
     }
}