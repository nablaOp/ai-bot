using Discord.Interactions;

namespace AiBot.Core;

/// <summary>
/// Simple module for InteractionService
/// 
/// https://discordnet.dev/guides/int_framework/intro.html?q=InteractionModuleBase
/// </summary>
public class CommandsModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("info", "Bot info")]
    public Task Info() => RespondAsync("I'm just a bot. Make me the bot!");
}