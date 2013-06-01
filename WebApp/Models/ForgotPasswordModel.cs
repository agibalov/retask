using FluentValidation.Attributes;
using WebApp.Models.Validation;

namespace WebApp.Models
{
    [Validator(typeof(ForgotPasswordModelValidator))]
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }
}