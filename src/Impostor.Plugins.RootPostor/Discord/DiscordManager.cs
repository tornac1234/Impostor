using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace Impostor.Plugins.RootPostor.Discord
{
    public class DiscordManager
    {
        public static DiscordManager Instance;
        private string TOKEN;
        private DiscordSocketClient _client;
        private DiscordCommandsHandler _commandsHandler;
        private CommandService commandService;

        public DiscordManager()
        {
            Instance = this;
            ReadToken();
            MainAsync().GetAwaiter().GetResult();
        }

        private void ReadToken()
        {
            using var reader = new StreamReader("config.json");
            var json = reader.ReadToEnd();
            TOKEN = JsonConvert.DeserializeObject<Config>(json).DiscordToken;
        }

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Info });
            commandService = new CommandService();

            _commandsHandler = new DiscordCommandsHandler(_client, commandService);
            await _commandsHandler.InstallCommandsAsync();

            _client.Log += Log;
            _client.Ready += () =>
            {
                Console.WriteLine($"Bot is ready [{_client.CurrentUser.Username}:{_client.CurrentUser.Discriminator}]");
                return Task.CompletedTask;
            };

            await _client.LoginAsync(TokenType.Bot, TOKEN);
            await _client.StartAsync();
        }

        public async Task ShutdownAsync()
        {
            await _client.LogoutAsync();
            Console.WriteLine("Logged out successfully");
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        class Config
        {
            public string DiscordToken;
        }
    }
}
