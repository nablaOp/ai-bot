namespace AiBot.Core;

public class Secrets
{
    /// <summary>
    /// Discord bot token
    ///
    /// https://discord.com/developers/docs/topics/oauth2#bot-users
    /// </summary>
    public string? BotToken { get; set; }

    /// <summary>
    /// Discord bot guild
    /// It's an optional setting. Can be configured for cases like:
    /// 1. For test purposes since Global Commands take around 1 hour to register
    /// 2. You'd like to use the bot for one server only
    /// 
    /// https://discord.com/developers/docs/interactions/application-commands
    /// https://en.wikipedia.org/wiki/Template:Discord_server#:~:text=Getting%20Guild%20ID,to%20get%20the%20guild%20ID.
    /// </summary>
    public ulong? BotGuild { get; set; }

    /// <summary>
    /// OpenAI API key
    /// 
    /// https://help.openai.com/en/articles/4936850-where-do-i-find-my-api-key
    /// </summary>
    public string? OpenAiKey { get; set; }
}
