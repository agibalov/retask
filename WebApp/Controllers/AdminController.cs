using System.Net;
using System.Web.Mvc;
using Service;
using Service.Exceptions;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly RetaskService _retaskService;
        private readonly RetaskServiceErrorMessageProvider _retaskServiceErrorMessageProvider;
        private readonly SessionTokenCookieManager _sessionTokenCookieManager;

        public AdminController(
            RetaskService retaskService,
            RetaskServiceErrorMessageProvider retaskServiceErrorMessageProvider,
            SessionTokenCookieManager sessionTokenCookieManager)
        {
            _retaskService = retaskService;
            _retaskServiceErrorMessageProvider = retaskServiceErrorMessageProvider;
            _sessionTokenCookieManager = sessionTokenCookieManager;
        }

        public ActionResult Index()
        {
            var getDebugDataResult = _retaskService.GetDebugData();
            if (!getDebugDataResult.Ok)
            {
                var errorMessage = _retaskServiceErrorMessageProvider.GetErrorMessage(getDebugDataResult.Error);
                ModelState.AddModelError("", errorMessage);
                return View();
            }

            return View(getDebugDataResult.Payload);
        }

        [HttpPost]
        public ActionResult RebuildDatabase()
        {
            var rebuildDatabaseResult = _retaskService.RebuildDatabase();
            if (!rebuildDatabaseResult.Ok)
            {
                var errorMessage = _retaskServiceErrorMessageProvider.GetErrorMessage(rebuildDatabaseResult.Error);
                ModelState.AddModelError("", errorMessage);
                return View();
            }

            return RedirectToAction("Index");
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            var httpRequest = filterContext.HttpContext.Request;

            var hasSessionTokenCookie = _sessionTokenCookieManager.HasSessionTokenCookie(httpRequest);
            if (!hasSessionTokenCookie)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            var sessionToken = _sessionTokenCookieManager.GetSessionToken(httpRequest);
            var getUserInfoResult = _retaskService.GetUserInfo(sessionToken);
            if (!getUserInfoResult.Ok)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                return;
            }

            var userIsAdmin = getUserInfoResult.Payload.IsAdmin;
            if (!userIsAdmin)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}
