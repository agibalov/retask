using System.Web.Http;
using System.Web.Mvc;
using Service;
using WebApp.Infrastructure;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SessionTokenCookieManager _sessionTokenCookieManager;
        private readonly RetaskService _retaskService;

        public HomeController(
            SessionTokenCookieManager sessionTokenCookieManager, 
            RetaskService retaskService)
        {
            _sessionTokenCookieManager = sessionTokenCookieManager;
            _retaskService = retaskService;
        }

        public ActionResult Index()
        {
            var hasSessionTokenCookie = _sessionTokenCookieManager.HasSessionTokenCookie(Request);
            if (hasSessionTokenCookie)
            {
                var sessionToken = _sessionTokenCookieManager.GetSessionToken(Request);
                var getUserInfoResult = _retaskService.GetUserInfo(sessionToken);
                if (getUserInfoResult.Ok)
                {
                    return RedirectToAction("Index", "App");
                }

                _sessionTokenCookieManager.UnsetSessionTokenCookie(Response.Cookies);
            }

            return View();
        }

        public ActionResult Disclaimer()
        {
            return View();
        }

        public ActionResult Robots()
        {
            Response.ContentType = "text/plain";
            return View();
        }

        public ActionResult Developers()
        {
            var apiExplorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();
            var model = ApiModelGenerator.BuildApiModel(apiExplorer);
            return View(model);
        }
    }
}
