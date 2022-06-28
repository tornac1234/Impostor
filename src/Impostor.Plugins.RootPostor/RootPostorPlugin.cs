using System.Threading.Tasks;
using Impostor.Api.Games;
using Impostor.Api.Games.Managers;
using Impostor.Api.Innersloth;
using Impostor.Api.Plugins;
using Impostor.Plugins.RootPostor.Discord;
using Microsoft.Extensions.Logging;

namespace Impostor.Plugins.Example
{
    [ImpostorPlugin("fr.rootcan.rootpostor")]
    public class RootPostorPlugin : PluginBase
    {
        public static RootPostorPlugin Instance;
        private readonly ILogger<RootPostorPlugin> _logger;
        private readonly IGameManager _gameManager;
        public DiscordManager DiscordManager;

        public RootPostorPlugin(ILogger<RootPostorPlugin> logger, IGameManager gameManager)
        {
            Instance = this;
            _logger = logger;
            _gameManager = gameManager;
        }

        public override async ValueTask EnableAsync()
        {
            _logger.LogInformation("RootPostor is being enabled.");

            DiscordManager = new DiscordManager();

            await CreateCustomGameAsync(15, "Tornac");
        }

        public override async ValueTask DisableAsync()
        {
            _logger.LogInformation("RootPostor is being disabled.");
            await DiscordManager.ShutdownAsync();
        }

        public async ValueTask<GameCode> CreateCustomGameAsync(int playerCount, GameCode gameCode)
        {
            var gameOptionsData = new GameOptionsData();
            gameOptionsData.MaxPlayers = (byte)playerCount;
            gameOptionsData.AnonymousVotes = true;
            gameOptionsData.NumImpostors = 2;
            gameOptionsData.KillCooldown = 40;
            gameOptionsData.VisualTasks = false;
            gameOptionsData.PlayerSpeedMod = 1;
            var game = await _gameManager.CreateAsync(null, new GameOptionsData(), gameCode);
            if (game == null)
            {
                _logger.LogWarning("Special game creation was cancelled");
                return null;
            }
            else
            {
                game.DisplayName = "TORNAC GAME";
                await game.SetPrivacyAsync(true);

                _logger.LogInformation("Created special game {0}.", game.Code.Code);
                return game.Code.Code;
            }
        }
    }
}
