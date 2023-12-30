using AiBot.Core;
using AiBot.Core.Configuration;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using OpenAI;
using OpenAI.Interfaces;
using OpenAI.Managers;

var builder = WebApplication.CreateSlimBuilder(args);

var discordSocketConfig = new DiscordSocketConfig
{
    GatewayIntents =
        GatewayIntents.Guilds |
        GatewayIntents.MessageContent |
        GatewayIntents.GuildMessages |
        GatewayIntents.GuildMessageReactions |
        GatewayIntents.DirectMessages |
        GatewayIntents.DirectMessageReactions |
        GatewayIntents.GuildEmojis
};

builder.Services.AddLogging(b => b.AddConsole()); 

// Add extended options to apply unique settings to our application
builder.Services.Configure<OpenAiExtendedOptions>(
    builder.Configuration.GetSection(OpenAiExtendedOptions.SectionName));
// Add default options to apply default settings to OpenAI SDK
builder.Services.Configure<OpenAiOptions>(
    builder.Configuration.GetSection(OpenAiOptions.SettingKey));
builder.Services.Configure<DiscordOptions>(
    builder.Configuration.GetSection(DiscordOptions.SectionName));

builder.Services
    .AddSingleton(discordSocketConfig)
    .AddSingleton<DiscordSocketClient>()
    .AddSingleton<InteractionService>()
    .AddSingleton<InteractionHandler>()
    .AddSingleton<HttpClient>(_ => new HttpClient()) // OpenAIService constructor requires HttpClient to work with IOptions
    .AddSingleton<IOpenAIService, OpenAIService>()
    .AddSingleton<DiscordClientService>();

var app = builder.Build();

var discordClientService = app.Services.GetRequiredService<DiscordClientService>();
await discordClientService.InitAndStartAsync();

const string url = "http://0.0.0.0:8080";
app.Run(url);