namespace PlayersLib
{
    using System.Linq;
    using CommonLib.Behaviours;
    using UnityEngine;

    /// <summary>
    /// Manages active players instances.
    /// </summary>
    public class PlayerManager : SingletonBehaviour<PlayerManager>
    {
        public static readonly LocalPlayerIndex InvalidLocalPlayerIndex = (LocalPlayerIndex)(-1);

        /// <summary>
        /// A collection of local, player instances.
        /// </summary>
        public Player[] LocalPlayers = new Player[4];

        public Color[] PlayerColors;

        public Player EnemyPlayer;
    
        public Player[] PlayingPlayers
        {
            get { return this.LocalPlayers.Where(_ => _.IsPlaying).ToArray(); }
        }

        /// <summary>
        /// Returns true if the player is a local player.
        /// </summary>
        /// <param name="player">The player to check.</param>
        public bool IsLocalPlayer(Player player)
        {
            return this.LocalPlayers.Contains(player);
        }

        public int GetLocalPlayerIndex(Player player)
        {
            if (player == null)
            {
                return -1;
            }

            for (var i = 0; i < 4; ++i)
            {
                if (this.LocalPlayers[i] == player)
                {
                    return i;
                }
            }
            return -1;
        }

        public LocalPlayerIndex GetLocalPlayerId(Player player)
        {
            return (LocalPlayerIndex)this.GetLocalPlayerIndex(player);
        }
    }

    public enum LocalPlayerIndex
    {
        First,
        Second,
        Third,
        Fourth
    }
}
