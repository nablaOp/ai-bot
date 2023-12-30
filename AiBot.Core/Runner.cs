using AiBot.Core.AiRequests;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace AiBot.Core;

public static class Runner
{
    public static async Task RunClients(IServiceProvider serviceProvider, IConfigurationRoot configurationRoot)
    {
        var client = serviceProvider.GetRequiredService<DiscordSocketClient>();
        var openAiService = serviceProvider.GetRequiredService<IOpenAIService>();

        client.MessageReceived += message =>
        {
            Task.Run(() => ProcessMessage(message, openAiService));
            
            return Task.CompletedTask;
        };

        await serviceProvider.GetRequiredService<InteractionHandler>().InitializeAsync();

        await client.LoginAsync(TokenType.Bot, configurationRoot.GetValue<string>(nameof(Secrets.BotToken)));
        await client.StartAsync();
    }

    private static async Task ProcessMessage(SocketMessage message, IOpenAIService openAiService)
    {
        var (systemMessage, userMessage) = IAiRequest.Create(message);
        var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(systemMessage),
                ChatMessage.FromUser(userMessage),
            },
            Model = Models.Gpt_4_1106_preview,
            MaxTokens = 255
        });

        if (completionResult.Successful)
        {
            var answer = completionResult.Choices.First().Message.Content;
            
            await message.Channel.SendMessageAsync($"{message.Author.Mention} {answer}");
        }
    }
}