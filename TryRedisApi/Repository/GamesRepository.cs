using TryRedisApi.Data;
using TryRedisApi.Models;

namespace TryRedisApi.Repository
{
    public class GamesRepository
    {
        public List<Game> GetAll() => GamesDb.GetThreeGames();
        public Game GetById() => GamesDb.GetThreeGames().Single(g=>g.Id == 1);
    }
}
