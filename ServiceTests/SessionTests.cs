using NUnit.Framework;
using Service.Exceptions;
using ServiceTests.Extensions;

namespace ServiceTests
{
    [TestFixture]
    public class SessionTests : AbstractTest
    {
        [Test]
        public void SessionExpiresIfIdleIsTooLong()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.ConfirmSignUpRequest(SignUpConfirmationNotifier.LastSecret).Ok();

            var sessionToken = Service.SignIn("loki2302@loki2302.loki2302", "qwerty123")
                .Ok()
                .HasSessionToken()
                .SessionToken;
            Service.Ping(sessionToken).Ok();

            TimeProvider.TickSeconds(SessionTimeToExpire);
            Service.Ping(sessionToken).Error(ServiceError.SessionExpired);
        }

        [Test]
        public void CanUsePingToKeepSessionAlive()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.ConfirmSignUpRequest(SignUpConfirmationNotifier.LastSecret).Ok();

            var sessionToken = Service.SignIn("loki2302@loki2302.loki2302", "qwerty123")
                .Ok()
                .HasSessionToken()
                .SessionToken;
            Service.GetUserInfo(sessionToken).Ok();

            TimeProvider.TickSeconds(SessionTtlSeconds / 2);
            Service.Ping(sessionToken);

            TimeProvider.TickSeconds(SessionTtlSeconds);
            Service.GetUserInfo(sessionToken).Ok();
        }
    }
}