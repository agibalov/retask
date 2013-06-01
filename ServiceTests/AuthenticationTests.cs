using NUnit.Framework;
using Service.Exceptions;
using ServiceTests.Extensions;

namespace ServiceTests
{
    [TestFixture]
    public class AuthenticationTests : AbstractTest
    {
        [Test]
        public void CantSignUpWithBadEmail()
        {
            Service.SignUp("loki2302@", "qwerty")
                .Error(ServiceError.ValidationError)
                .FieldsInError("Email");
        }

        [Test]
        public void CantSignUpWithBadPassword()
        {
            Service.SignUp("loki2302@loki2302.loki2302", "1")
                .Error(ServiceError.ValidationError)
                .FieldsInError("Password");
        }

        [Test]
        public void CantSignUpWithBothEmailAndPasswordInvalid()
        {
            Service.SignUp("loki2302@", "1")
                .Error(ServiceError.ValidationError)
                .FieldsInError("Email", "Password");
        }

        [Test]
        public void CanSignUpWhenEmailAlreadyUsedButNotConfirmed()
        {
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty111").Ok();
        }

        [Test]
        public void CantSignUpWhenEmailAlreadyUsedAndConfirmed()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            var secret = SignUpConfirmationNotifier.LastSecret.AssertNotEmptyString("Secret");
            Service.ConfirmSignUpRequest(secret).Ok();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty").Error(ServiceError.UserAlreadyRegistered);
        }

        [Test]
        public void CantSignInWithoutSigningUp()
        {
            Service.SignIn("loki2302@loki2302.loki2302", "qwerty123")
                .Error(ServiceError.NoSuchUser);
        }

        [Test]
        public void CanSignUp()
        {
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
        }

        [Test]
        public void CantSignInWithoutSignUpConfirmation()
        {
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty").Ok();
            Service.SignIn("loki2302@loki2302.loki2302", "qwerty").Error(ServiceError.NoSuchUser);
        }

        [Test]
        public void CanSignInWithSignUpConfirmation()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Assert.AreEqual("loki2302@loki2302.loki2302", SignUpConfirmationNotifier.LastEmail);
            var secret = SignUpConfirmationNotifier.LastSecret.AssertNotEmptyString("Secret");

            WelcomeNotifier.Reset();
            Service.ConfirmSignUpRequest(secret).Ok();
            Assert.AreEqual("loki2302@loki2302.loki2302", WelcomeNotifier.LastEmail);

            Service.SignIn("loki2302@loki2302.loki2302", "qwerty123").Ok();
        }

        [Test]
        public void CanGetUserInfoWithValidSessionToken()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            var secret = SignUpConfirmationNotifier.LastSecret.AssertNotEmptyString("Secret");
            Service.ConfirmSignUpRequest(secret).Ok();
            var sessionToken = Service.SignIn("loki2302@loki2302.loki2302", "qwerty123").Ok().SessionToken;

            Service.GetUserInfo(sessionToken).Ok()
                .HasGoodUserId()
                .HasEmail("loki2302@loki2302.loki2302");
        }

        [Test]
        public void CantGetUserInfoWithBadSessionToken()
        {
            Service.GetUserInfo("qwerty").Error(ServiceError.SessionExpired);
        }

        [Test]
        public void CanChangePasswordWithValidSessionToken()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.ConfirmSignUpRequest(SignUpConfirmationNotifier.LastSecret).Ok();

            var sessionToken = Service.SignIn("loki2302@loki2302.loki2302", "qwerty123")
                .Ok()
                .HasSessionToken()
                .SessionToken;

            Service.ChangePassword(sessionToken, "qwerty123", "qwerty").Ok();
            Service.SignIn("loki2302@loki2302.loki2302", "qwerty").Ok();
            Service.SignIn("loki2302@loki2302.loki2302", "qwerty123").Error(ServiceError.InvalidPassword);
        }

        [Test]
        public void CantChangePasswordWithBadSessionToken()
        {
            Service.ChangePassword("qwerty", "qwerty123", "qwerty").Error(ServiceError.SessionExpired);
        }

        [Test]
        public void CanResetPasswordWithValidSecret()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.ConfirmSignUpRequest(SignUpConfirmationNotifier.LastSecret).Ok();

            ResetPasswordNotifier.Reset();
            Service.RequestPasswordReset("loki2302@loki2302.loki2302").Ok();
            Service.ResetPassword(ResetPasswordNotifier.LastSecret, "qwerty").Ok();

            Service.SignIn("loki2302@loki2302.loki2302", "qwerty").Ok();
            Service.SignIn("loki2302@loki2302.loki2302", "qwerty123").Error(ServiceError.InvalidPassword);
        }

        [Test]
        public void CantResetPasswordTwiceUsingSameSecret()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.ConfirmSignUpRequest(SignUpConfirmationNotifier.LastSecret).Ok();

            ResetPasswordNotifier.Reset();
            Service.RequestPasswordReset("loki2302@loki2302.loki2302").Ok();
            Service.ResetPassword(ResetPasswordNotifier.LastSecret, "qwerty").Ok();
            Service.ResetPassword(ResetPasswordNotifier.LastSecret, "qwerty1").Error(ServiceError.NoSuchPendingPasswordResetRequest);
        }

        [Test]
        public void CantResetPasswordWithBadSecret()
        {
            Service.ResetPassword("qwerty", "qwerty").Error(ServiceError.NoSuchPendingPasswordResetRequest);
        }
    }
}
