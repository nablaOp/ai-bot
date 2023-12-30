using AiBot.Core.AiRequests;
using AiBot.Core.Configuration;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;

namespace AiBot.Core;

public class DiscordClientService(
    DiscordSocketClient discordClient,
    IOpenAIService openAiService,
    InteractionHandler interactionHandler,
    ILogger<DiscordClientService> logger,
    IOptions<DiscordOptions> discordOptions,
    IOptions<OpenAiExtendedOptions> openAiOptions)
{
    private readonly DiscordOptions _discordOptions = discordOptions.Value;
    private readonly OpenAiExtendedOptions _openAiOptions = openAiOptions.Value;

    public async Task InitAndStartAsync()
    {
        discordClient.Log += message =>
        {
            logger.Log(message);
            return Task.CompletedTask;
        };
        
        discordClient.MessageReceived += message =>
        {
            Task.Run(() => ProcessMessageAsync(message));
            return Task.CompletedTask;
        };

        await interactionHandler.InitializeAsync();

        await discordClient.LoginAsync(TokenType.Bot, _discordOptions.BotToken);
        await discordClient.StartAsync();
    }

    private async Task ProcessMessageAsync(SocketMessage message)
    {
        var request = IAiRequest.Create(message, _discordOptions.BotUsername);

        if (request is null) return;

        var (systemMessage, userMessage) = request;

        if (string.IsNullOrEmpty(systemMessage) || string.IsNullOrEmpty(userMessage)) return;

        var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(systemMessage),
                ChatMessage.FromUser(userMessage),
            },
            Model = _openAiOptions.DefaultModelId,
            MaxTokens = _openAiOptions.DefaultMaxTokens
        });

        if (completionResult.Successful)
        {
            var answer = completionResult.Choices.First().Message.Content;

            await message.Channel.SendMessageAsync($"{message.Author.Mention} {answer}");
        }
    }
}