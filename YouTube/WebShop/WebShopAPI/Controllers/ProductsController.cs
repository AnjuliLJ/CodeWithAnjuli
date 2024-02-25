using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using WebShopAPI.Models;

namespace WebShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var list = new List<Product>
            {
                new Product() { Name = "Dress", Description = "A dress with pockets" },
                new Product() { Name = "Jeans", Description = "Jeans with pockets big enough to hold your phone" }
            };
            return list;
        }
    }
}
