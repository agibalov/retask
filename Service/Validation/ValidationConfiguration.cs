using FluentValidation;

namespace Service.Validation
{
    public static class ValidationConfiguration
    {
        public static IRuleBuilder<T, string> GoodEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .EmailAddress()
                .Length(5, 128);
        }

        public static IRuleBuilder<T, string> GoodPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .Length(6, 32);
        }

        public static IRuleBuilder<T, string> GoodSessionToken<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty();
        }

        public static IRuleBuilder<T, string> GoodConfirmationSecret<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty();
        }

        public static IRuleBuilder<T, string> GoodResetPasswordSecret<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty();
        }

        public static IRuleBuilder<T, int> GoodId<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThan(0);
        }

        public static IRuleBuilder<T, string> GoodTaskDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .Length(1, 10240);
        }
    }
}