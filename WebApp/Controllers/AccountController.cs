using System.Web.Mvc;
using Service;
using Service.Exceptions;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly RetaskService _retaskService;
        private readonly SessionTokenCookieManager _sessionTokenCookieManager;
        private readonly RetaskServiceErrorMessageProvider _retaskServiceErrorMessageProvider;

        public AccountController(
            RetaskService retaskService,
            SessionTokenCookieManager sessionTokenCookieManager,
            RetaskServiceErrorMessageProvider retaskServiceErrorMessageProvider)
        {
            _retaskService = retaskService;
            _sessionTokenCookieManager = sessionTokenCookieManager;
            _retaskServiceErrorMessageProvider = retaskServiceErrorMessageProvider;
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignInModel signInModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(signInModel);
            }

            var signInResult = _retaskService.SignIn(signInModel.Email, signInModel.Password);
            if (signInResult.Ok)
            {
                var sessionToken = signInResult.Payload.SessionToken;
                _sessionTokenCookieManager.SetSessionTokenCookie(Response.Cookies, sessionToken);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = _retaskServiceErrorMessageProvider.GetErrorMessage(signInResult.Error);

            return View(signInModel);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpModel signUpModel)
        {
            if (!ModelState.IsValid)
            {
                return View(signUpModel);
            }

            var signUpResult = _retaskService.SignUp(signUpModel.Email, signUpModel.Password);
            if(signUpResult.Ok)
            {
                return RedirectToAction("SignedUp");
            }

            ViewBag.Message = _retaskServiceErrorMessageProvider.GetErrorMessage(signUpResult.Error);

            return View(signUpModel);
        }

        [HttpGet]
        public ActionResult SignedUp()
        {
            return View();
        }

        public ActionResult SignOut()
        {
            _sessionTokenCookieManager.UnsetSessionTokenCookie(Response.Cookies);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Confirm(string secret)
        {
            var confirmSignUpRequestResult = _retaskService.ConfirmSignUpRequest(secret);
            if (confirmSignUpRequestResult.Ok)
            {
                return RedirectToAction("Confirmed");
            }

            ViewBag.Message = _retaskServiceErrorMessageProvider.GetErrorMessage(confirmSignUpRequestResult.Error);

            return View();
        }

        [HttpGet]
        public ActionResult Confirmed()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordModel);
            }

            var requestPasswordResetResult = _retaskService.RequestPasswordReset(forgotPasswordModel.Email);
            if (requestPasswordResetResult.Ok)
            {
                return View("ForgotPasswordLinkSent", forgotPasswordModel);
            }

            ViewBag.Message = _retaskServiceErrorMessageProvider.GetErrorMessage(requestPasswordResetResult.Error);

            return View();
        }

        [HttpGet]
        public ActionResult Reset(string secret)
        {
            var resetPasswordModel = new ResetPasswordModel
                {
                    Secret = secret
                };
            
            return View(resetPasswordModel);
        }

        [HttpPost]
        public ActionResult Reset(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordModel);
            }

            var resetPasswordResult = _retaskService.ResetPassword(
                resetPasswordModel.Secret, 
                resetPasswordModel.Password);
            if (resetPasswordResult.Ok)
            {
                return View("Resetted");
            }

            ViewBag.Message = _retaskServiceErrorMessageProvider.GetErrorMessage(resetPasswordResult.Error);

            return View();
        }
    }
}
