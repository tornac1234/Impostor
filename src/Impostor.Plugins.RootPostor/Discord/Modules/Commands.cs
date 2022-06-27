using System;
using System.Threading.Tasks;
using Discord.Commands;
using Impostor.Plugins.Example;

namespace Impostor.Plugins.RootPostor.Discord.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("startgame")]
        [Alias("sg")]
        [Summary("Starts an AMOGUS game")]
        public async Task StartGameAsync([Summary("The number of players")] int num, string gameCode)
        {
            var newCode = await RootPostorPlugin.Instance.CreateCustomGameAsync(num, gameCode);
            Console.WriteLine($"New code: {newCode}");
            if (newCode != null)
            {
                await Context.Channel.SendMessageAsync($"Starting an AMOGUS game right now for {num} players with code {gameCode}");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"Couldn't create an AMOGUS game with the code {gameCode}");
            }
        }

        [Command("link")]
        [Summary("Links an Epic Games account to a Discord Account")]
        public async Task LinkAsync([Summary("The Epic Games identifier")] string epicGamesId)
        {
            if (epicGamesId.Length > 0)
            {
                var userInfo = Context.Client.CurrentUser;
                await ReplyAsync($"Linking **{userInfo.Username}#{userInfo.Discriminator}** to ``{epicGamesId}``");
            }
            else
            {
                await ReplyAsync("You need to provide a valid Epic Games ID");
            }
        }
    }
}
