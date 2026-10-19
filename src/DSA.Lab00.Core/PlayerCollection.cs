using System;
using System.Collections.Generic;

namespace DSA.Lab00
{
    public sealed class PlayerCollection
    {
        private readonly List<Player> _players;

        public PlayerCollection(IEnumerable<Player> players)
        {
            if (players == null)
            {
                throw new ArgumentNullException(nameof(players));
            }

            _players = new List<Player>(players);
        }

        public Player? FindById(int id)
        {
            foreach (Player player in _players)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }

            return null;
        }

        public Player? FindHighestScore()
        {
            if (_players.Count == 0)
            {
                return null;
            }
            
            Player highestScorer = _players[0];
            for (var i = 1; i < _players.Count; i++)
            {
                if (_players[i].Score > highestScorer.Score)
                {
                    highestScorer = _players[i];
                }
            }

            return highestScorer;
        }

        public bool ContainsName(string name)
        {
            
            return false;
        }
    }
}
