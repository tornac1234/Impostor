using System;
using System.Numerics;
using System.Threading.Tasks;
using Impostor.Api.Events;
using Impostor.Api.Events.Player;
using Impostor.Api.Innersloth.Customization;
using Microsoft.Extensions.Logging;

namespace Impostor.Plugins.Example.Handlers
{
    public class PlayerEventListener : IEventListener
    {
        private readonly Random _random = new Random();

        private readonly ILogger<PlayerEventListener> _logger;

        public PlayerEventListener(ILogger<PlayerEventListener> logger)
        {
            _logger = logger;
        }

        [EventListener]
        public void OnPlayerSpawned(IPlayerSpawnedEvent e)
        {
            _logger.LogInformation($"Player {e.PlayerControl.PlayerInfo.PlayerName} [Id: {e.ClientPlayer.Client.Id}] > spawned");
        }

        [EventListener]
        public void OnPlayerDestroyed(IPlayerDestroyedEvent e)
        {
            _logger.LogInformation("Player {player} > destroyed", e.PlayerControl.PlayerInfo.PlayerName);
        }

        [EventListener]
        public async ValueTask OnPlayerChatAsync(IPlayerChatEvent e)
        {
            _logger.LogInformation("Player {player} > said {message}", e.PlayerControl.PlayerInfo.PlayerName, e.Message);

            if ((e.Message == "randomlook" || e.Message == "rl"))
            {
                e.IsCancelled = true;
                if (e.Game.GameState != Api.Innersloth.GameStates.NotStarted && e.Game.GameState != Api.Innersloth.GameStates.Starting)
                    return;
                var playerControl = e.PlayerControl;
                await playerControl.SetColorAsync((ColorType)_random.Next(0, 17 + 1));
                await playerControl.SetHatAsync((HatType)_random.Next(0, 114 + 1));
                await playerControl.SetSkinAsync((SkinType)_random.Next(0, 18 + 1));
                await playerControl.SetPetAsync((PetType)_random.Next(0, 11 + 1));
            }

            /* if (e.Message == "test")
            {
                e.Game.Options.KillCooldown = 0;
                e.Game.Options.NumImpostors = 2;
                e.Game.Options.PlayerSpeedMod = 5;

                await e.Game.SyncSettingsAsync();
            }

            if (e.Message == "look")
            {
                await e.PlayerControl.SetColorAsync(ColorType.Pink);
                await e.PlayerControl.SetHatAsync(HatType.Cheese);
                await e.PlayerControl.SetSkinAsync(SkinType.Police);
                await e.PlayerControl.SetPetAsync(PetType.Ufo);
            }

            if (e.Message == "snap")
            {
                await e.PlayerControl.NetworkTransform.SnapToAsync(new Vector2(1, 1));
            }

            if (e.Message == "completetasks")
            {
                foreach (var task in e.PlayerControl.PlayerInfo.Tasks)
                {
                    await task.CompleteAsync();
                }
            }*/
        }

        [EventListener]
        public void OnPlayerStartMeetingEvent(IPlayerStartMeetingEvent e)
        {
            _logger.LogInformation("Player {player} > started meeting, reason: {reason}", e.PlayerControl.PlayerInfo.PlayerName, e.Body == null ? "Emergency call button" : "Found the body of the player " + e.Body.PlayerInfo.PlayerName);
        }

        [EventListener]
        public void OnPlayerEnterVentEvent(IPlayerEnterVentEvent e)
        {
            _logger.LogInformation("Player {player} entered the vent in {vent}", e.PlayerControl.PlayerInfo.PlayerName, e.Vent.Name);
        }

        [EventListener]
        public void OnPlayerExitVentEvent(IPlayerExitVentEvent e)
        {
            _logger.LogInformation("Player {player} exited the vent in {vent}", e.PlayerControl.PlayerInfo.PlayerName, e.Vent.Name);
        }

        [EventListener]
        public void OnPlayerVentEvent(IPlayerVentEvent e)
        {
            _logger.LogInformation("Player {player} vented to {vent}", e.PlayerControl.PlayerInfo.PlayerName, e.NewVent.Name);
        }

        [EventListener]
        public void OnPlayerVoted(IPlayerVotedEvent e)
        {
            _logger.LogDebug("Player {player} voted for {type} {votedFor}", e.PlayerControl.PlayerInfo.PlayerName, e.VoteType, e.VotedFor?.PlayerInfo.PlayerName);
        }

        [EventListener]
        public void OnPlayerCompletedTaskEvent(IPlayerCompletedTaskEvent e)
        {
            _logger.LogInformation("Player {player} completed {task}, {type}, {category}, visual {visual}", e.PlayerControl.PlayerInfo.PlayerName, e.Task.Task.Name, e.Task.Task.Type, e.Task.Task.Category, e.Task.Task.IsVisual);
        }
    }
}
