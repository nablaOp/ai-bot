# A simple Discord bot integrated with OpenAI API

## Run

```
docker build -t ai-bot .
```
```
docker run \
-e OpenAIServiceOptions__ApiKey=[OPEN_AI_KEY] \
-e Discord__BotToken=[YOUR_DISCORD_BOT_TOKEN] \
-e Discord__BotGuild=[YOUR_DISCORD_GUILD_ID] \
-e Discord__BotUsername=[YOUR_BOT_USER_NAME] \
ai-bot
```

`[OPEN_AI_KEY]` API key for OpenAI API.

`[YOUR_DISCORD_BOT_TOKEN]` Token of your Discord bot. You can get it by creating a new bot in Discord Developer Portal. 

`[YOUR_DISCORD_GUILD_ID]` ID of your Discord server. To faster test commands on your server and if you want to use the bot only on your server, you can create a new server and invite the bot to it. You can get the ID by right-clicking on your server icon and selecting "Copy ID".

`[YOU_BOT_USER_NAME]` Name of your bot user. To handle bot mentioned.

## Usage

`/info` an example command

`@bot_name [message]` to send a message to the bot

## Dependencies

- [Discord.Net](https://github.com/discord-net/Discord.Net)
- [Betalgo OpenAI](https://github.com/betalgo/openai)
