using FluentValidation;
using Service.DAL;
using Service.DTO;
using Service.TransactionScripts.BL;
using Service.Validation.Requests;
using Service.Validation.Validators;

namespace Service.TransactionScripts
{
    public class GetUserInfoTransactionScript
    {
        private readonly AuthenticationService _authenticationService;
        private readonly GetUserInfoRequestValidator _getUserInfoRequestValidator;

        public GetUserInfoTransactionScript(
            AuthenticationService authenticationService, 
            GetUserInfoRequestValidator getUserInfoRequestValidator)
        {
            _authenticationService = authenticationService;
            _getUserInfoRequestValidator = getUserInfoRequestValidator;
        }

        public UserInfoDTO GetUserInfo(TodoContext context, GetUserInfoRequest getUserInfoRequest)
        {
            _getUserInfoRequestValidator.ValidateAndThrow(getUserInfoRequest);

            var user = _authenticationService.GetUserBySessionToken(
                context, 
                getUserInfoRequest.SessionToken);

            return new UserInfoDTO
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    IsAdmin = user.IsAdmin
                };
        }
    }
}