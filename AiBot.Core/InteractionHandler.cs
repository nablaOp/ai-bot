using AiBot.Core.Configuration;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Options;

namespace AiBot.Core;

public class InteractionHandler(
    DiscordSocketClient client,
    InteractionService interactionService,
    IServiceProvider services,
    IOptions<DiscordOptions> discordOptions)
{
    private readonly DiscordOptions _discordOptions = discordOptions.Value;

    public async Task InitializeAsync()
    {
        client.Ready += ReadyAsync;
        client.InteractionCreated += HandleInteraction;

        await RegisterModulesAsync();
    }

    private async Task ReadyAsync()
    {
        if (_discordOptions.BotGuild.HasValue)
            await interactionService.RegisterCommandsToGuildAsync(_discordOptions.BotGuild.Value);
        else
            await interactionService.RegisterCommandsGloballyAsync();
    }

    private async Task HandleInteraction(SocketInteraction interaction)
    {
        try
        {
            var context = new SocketInteractionContext(client, interaction);

            var result = await interactionService.ExecuteCommandAsync(context, services);

            if (!result.IsSuccess)
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        // implement
                        break;
                }
        }
        catch
        {
            if (interaction.Type is InteractionType.ApplicationCommand)
                await interaction.GetOriginalResponseAsync()
                    .ContinueWith(async msg => await msg.Result.DeleteAsync());
        }
    }

    private async Task RegisterModulesAsync()
    {
        await interactionService.AddModuleAsync<CommandsModule>(services);
    }
}