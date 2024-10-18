using Microsoft.AspNetCore.Mvc;
using TryRedisApi.Models;
using TryRedisApi.Repository;
using TryRedisApi.Services;

namespace TryRedisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GamesRepository _gamesRepository;
        private readonly RedisService _redisService;
        private bool _isFromCache = false;

        public GamesController(GamesRepository gamesRepository, RedisService redisService)
        {
            this._gamesRepository = gamesRepository;
            this._redisService = redisService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var instanceId = GetInstanceId();
            var cacheKey = $"Games_Cache_{instanceId}";

            var games = _redisService.GetCachedData<List<Game>>(cacheKey);
            if (games == null)
            {
                games = _gamesRepository.GetAll();
                _redisService.SaveCachedData(cacheKey, games, TimeSpan.FromSeconds(180));
                _isFromCache = false;
            }
            else
            {
                _isFromCache = true;
            }
            return Ok(games);
        }

        private string GetInstanceId()
        {
            var instanceId = HttpContext.Session.GetString("InstanceId");
            if (string.IsNullOrEmpty(instanceId))
            {
                instanceId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("InstanceId", instanceId);
            }
            return instanceId;
        }
    }
}
