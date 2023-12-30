using Discord.WebSocket;

namespace AiBot.Core.AiRequests;

public record SimpleQuestion(SocketMessage Message) : IAiRequest
{
    public string SystemMessage => """
                                   You are helpful assistant. You kindly answer questions.
                                   """;

    public string UserMessage => PureMessage;
    
    private string PureMessage =>
        Message.MentionedUsers.Aggregate(Message.Content,
            (current, mentionedUser) => current.Replace(mentionedUser.Mention, string.Empty));
}