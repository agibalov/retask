using NUnit.Framework;
using Service.DTO;

namespace ServiceTests.Extensions
{
    public static class UserInfoDTOAssertExtensions
    {
        public static UserInfoDTO Ok(this ServiceResult<UserInfoDTO> result)
        {
            var userInfo = result.Ok<UserInfoDTO>();
            Assert.IsNotNull(userInfo);
            return userInfo;
        }

        public static UserInfoDTO HasGoodUserId(this UserInfoDTO userInfo)
        {
            userInfo.UserId.AssertAdequateId("userid");
            return userInfo;
        }

        public static UserInfoDTO HasEmail(this UserInfoDTO userInfo, string email)
        {
            Assert.AreEqual(email, userInfo.Email);
            return userInfo;
        }
    }
}