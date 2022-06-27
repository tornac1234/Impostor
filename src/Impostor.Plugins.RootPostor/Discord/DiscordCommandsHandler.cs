using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Impostor.Plugins.RootPostor.Discord.Modules;

namespace Impostor.Plugins.RootPostor.Discord
{
    public class DiscordCommandsHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly char PREFIX = '!';

        public DiscordCommandsHandler(DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModuleAsync(typeof(Commands), null);
            foreach (var command in _commands.Modules)
            {
                Console.WriteLine($"Found command: {command.Name}");
            }
        }

        private async Task HandleCommandAsync(SocketMessage socketMessage)
        {
            var message = (SocketUserMessage)socketMessage;
            if (message == null) return;

            int argPos = 0;

            if (!message.HasCharPrefix(PREFIX, ref argPos)) return;
            if (message.Author.IsBot) return;

            var context = new SocketCommandContext(_client, message);
            Console.WriteLine($"HandleCommandAsync({socketMessage})");

            var result = await _commands.ExecuteAsync(context, argPos, null);

            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync($"Error: {result.Error.Value}");
        }
    }
}
