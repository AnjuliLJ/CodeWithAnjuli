using Microsoft.AspNetCore.Mvc;

namespace MovieAPI.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        [HttpGet]
        // [RequiredScopeOrAppPermission(RequiredScopesConfigurationKey = "EntraID:Scopes:Read")]
        public string Get()
        {
            var random = new Random();
            var randomNumber = random.Next(3);
            switch (randomNumber)
            {
                case 0:
                    return "Loki";
                case 1:
                    return "Avengers: Endgame";
                case 2:
                    return "Spider-Man: Far From Home";
                default:
                    return "Captain America: The First Avenger";
            }
        }

    }
}
