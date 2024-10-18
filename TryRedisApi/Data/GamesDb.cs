using TryRedisApi.Models;

namespace TryRedisApi.Data
{
    public static class GamesDb
    {
        public static List<Game> GetThreeGames()
        {
            List<Game> games = new List<Game>
        {
            new Game
            {
                Id = 1,
                Title = "Game One",
                Genre = "Action",
                Platform = "PC",
                Release = 2020
            },
            new Game
            {
                Id = 2,
                Title = "Game Two",
                Genre = "RPG",
                Platform = "Console",
                Release = 2021
            },
            new Game
            {
                Id = 3,
                Title = "Game Three",
                Genre = "Adventure",
                Platform = "Mobile",
                Release = 2022
            }
        };

            return games;
        }
    }
}
