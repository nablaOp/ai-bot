using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace AiBot.Core;

public class InteractionHandler(
    DiscordSocketClient client, 
    InteractionService interactionService, 
    IServiceProvider services,
    IConfiguration config)
{
    public async Task InitializeAsync()
    {
        client.Ready += ReadyAsync;
        client.InteractionCreated += HandleInteraction;
        
        await RegisterModulesAsync();
    }

    private async Task ReadyAsync()
    {
        var botGuild = config.GetValue<ulong?>(nameof(Secrets.BotGuild));

        if (botGuild.HasValue)
            await interactionService.RegisterCommandsToGuildAsync(botGuild.Value);
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
