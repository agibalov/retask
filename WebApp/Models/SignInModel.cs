using FluentValidation.Attributes;
using WebApp.Models.Validation;

namespace WebApp.Models
{
    [Validator(typeof(SignInModelValidator))]
    public class SignInModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}