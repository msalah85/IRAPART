using IRACMS.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace IRACMS.Web.Host.Controllers
{
    public class AntiForgeryController : IRACMSControllerBase
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