using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace MovieAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        [RequiredScopeOrAppPermission(RequiredScopesConfigurationKey = "AzureAD:Scopes:Read")]
        [HttpGet]
        public string Get()
        {
            return "Avengers: Endgame";
        }
    }
}
