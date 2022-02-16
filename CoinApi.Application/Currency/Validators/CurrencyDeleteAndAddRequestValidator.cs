using CoinApi.Domain.Currency.Models;
using FluentValidation;

namespace CoinApi.Application.Currency.Validators
{
    public class CurrencyDeleteAndAddRequestValidator : AbstractValidator<CurrencyDeleteAndAddRequest>
    {
        public CurrencyDeleteAndAddRequestValidator()
        {
            RuleFor(v => v.Name)
                .NotNull().NotEmpty().WithMessage("Name is required.");

            RuleFor(v => v.Sign)
                .NotNull().NotEmpty().WithMessage("Sign is required.");

            RuleFor(v => v.Symbol)
                .NotNull().NotEmpty().WithMessage("Symbol is required.");
        }
    }
}