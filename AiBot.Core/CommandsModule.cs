using AiBot.Core;

/// <summary>
/// Simple module for InteractionService
/// <see cref="https://discordnet.dev/guides/int_framework/intro.html?q=InteractionModuleBase"/>
/// </summary>
public class CommandsModule() : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("info", "Bot info")]
    public Task Ask() => RespondAsync("I'm just a bot. Make me the bot!");
}
