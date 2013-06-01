using FluentValidation.Attributes;
using WebApp.Models.Validation;

namespace WebApp.Models
{
    [Validator(typeof(SignUpModelValidator))]
    public class SignUpModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}