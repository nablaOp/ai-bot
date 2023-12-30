using Discord.WebSocket;

namespace AiBot.Core.AiRequests;

public interface IAiRequest
{
    string SystemMessage { get; }
    
    string UserMessage { get; }
    
    public static IAiRequest Create(SocketMessage message)
    {
        return new SimpleQuestion(message);
    }

    public void Deconstruct(out string systemMessage, out string userMessage) =>
        (systemMessage, userMessage) = (SystemMessage, UserMessage);
}