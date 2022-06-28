using System.Collections.Generic;
using System.Threading.Tasks;
using Impostor.Api.Innersloth;
using Impostor.Api.Net;

namespace Impostor.Api.Games.Managers
{
    public interface IGameManager
    {
        IEnumerable<IGame> Games { get; }

        IGame? Find(GameCode code);

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <param name="owner">Owner of the game</param>
        /// <param name="options">Game options.</param>
        /// <param name="code">Custom code</param>
        /// <returns>Created game or null if creation was cancelled by a plugin.</returns>
        /// <exception cref="ImpostorException">Thrown when game creation failed.</exception>
        ValueTask<IGame?> CreateAsync(IClient? owner, GameOptionsData options, GameCode? code = null);
    }
}
