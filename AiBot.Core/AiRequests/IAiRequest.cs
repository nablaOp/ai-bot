using Discord.WebSocket;

namespace AiBot.Core.AiRequests;

public interface IAiRequest
{
    string SystemMessage { get; }
    
    string UserMessage { get; }
    
    public static IAiRequest? Create(SocketMessage message, string? botUsername)
    {
        if (HasContentToIgnore(message))
            return null;
        
        var botMentioned = message.MentionedUsers.Any(u => u.IsBot && u.Username == botUsername);
       
        return botMentioned ? new SimpleQuestion(message) : null;
    }
    
    private static bool HasContentToIgnore(SocketMessage message)
    {
        return message.Author.IsBot || message.Content.StartsWith('/');
    }

    public void Deconstruct(out string systemMessage, out string userMessage) =>
        (systemMessage, userMessage) = (SystemMessage, UserMessage);
}