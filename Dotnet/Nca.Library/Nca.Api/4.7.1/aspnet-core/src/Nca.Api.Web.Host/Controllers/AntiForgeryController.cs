using Microsoft.AspNetCore.Antiforgery;
using Nca.Api.Controllers;

namespace Nca.Api.Web.Host.Controllers
{
    public class AntiForgeryController : ApiControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
