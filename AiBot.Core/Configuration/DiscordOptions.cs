using AiBot.Core.AiRequests;

namespace AiBot.Core.Configuration;

/// <summary>
/// Discord integration options
/// </summary>
public class DiscordOptions
{
    public const string SectionName = "Discord";
    
    /// <summary>
    /// Bot token
    ///
    /// https://discord.com/developers/docs/topics/oauth2#bot-users
    /// </summary>
    public string? BotToken { get; set; }

    /// <summary>
    /// Bot guild
    /// It's an optional setting. Can be configured for cases like:
    /// 1. For test purposes since Global Commands take around 1 hour to register
    /// 2. You'd like to use the bot for one server only
    /// 
    /// https://discord.com/developers/docs/interactions/application-commands
    /// https://en.wikipedia.org/wiki/Template:Discord_server#:~:text=Getting%20Guild%20ID,to%20get%20the%20guild%20ID.
    /// </summary>
    public ulong? BotGuild { get; set; }

    /// <summary>
    /// Bot username
    /// To be used in <see cref="IAiRequest.Create"/> to identify bot mentions
    /// </summary>
    public string? BotUsername { get; set; }
}