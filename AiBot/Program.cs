using AiBot.Core;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using OpenAI;
using OpenAI.Interfaces;
using OpenAI.Managers;

var builder = WebApplication.CreateSlimBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton(new DiscordSocketConfig
    {
        GatewayIntents = 
            GatewayIntents.Guilds |
            GatewayIntents.MessageContent | 
            GatewayIntents.GuildMessages |
            GatewayIntents.GuildMessageReactions | 
            GatewayIntents.DirectMessages | 
            GatewayIntents.DirectMessageReactions |
            GatewayIntents.GuildEmojis
    })
    .AddSingleton<DiscordSocketClient>()
    .AddSingleton<InteractionService>()
    .AddSingleton<InteractionHandler>()
    .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
    .AddSingleton(new OpenAiOptions
    {
        ApiKey = configuration.GetValue<string>(nameof(Secrets.OpenAiKey)) ?? string.Empty
    })
    .AddSingleton<IOpenAIService, OpenAIService>();

const string url = "http://0.0.0.0:8080";

var app = builder.Build();

var client = app.Services.GetRequiredService<DiscordSocketClient>();
client.Log += message =>
{
    Console.WriteLine(message.ToString());
    return Task.CompletedTask;
};

await Runner.RunClients(app.Services, configuration);

app.Run(url);