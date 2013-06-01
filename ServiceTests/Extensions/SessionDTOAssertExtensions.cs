using NUnit.Framework;
using Service.DTO;

namespace ServiceTests.Extensions
{
    public static class SessionDTOAssertExtensions
    {
        public static SessionDTO Ok(this ServiceResult<SessionDTO> result)
        {
            var session = result.Ok<SessionDTO>();
            Assert.IsNotNull(session);
            return session;
        }

        public static SessionDTO HasSessionToken(this SessionDTO session)
        {
            session.SessionToken.AssertNotEmptyString("Expected session token to be not empty");
            return session;
        }
    }
}