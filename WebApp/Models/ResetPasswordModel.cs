using FluentValidation.Attributes;
using WebApp.Models.Validation;

namespace WebApp.Models
{
    [Validator(typeof(ResetPasswordModelValidator))]
    public class ResetPasswordModel
    {
        public string Secret { get; set; }
        public string Password { get; set; }
    }
}