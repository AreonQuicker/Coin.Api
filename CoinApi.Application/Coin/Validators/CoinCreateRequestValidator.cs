using CoinApi.Domain.Coin.Models;
using FluentValidation;

namespace CoinApi.Application.Coin.Validators
{
    /// <summary>
    /// Create coin validation rules
    /// </summary>
    public class CoinCreateRequestValidator : AbstractValidator<CoinCreateRequest>
    {
        public CoinCreateRequestValidator()
        {
            RuleFor(v => v.Name)
                .NotNull().NotEmpty().WithMessage("Name is required.");

            RuleFor(v => v.Rank)
                .NotNull().NotEmpty().WithMessage("Rank is required.");

            RuleFor(v => v.Symbol)
                .NotNull().NotEmpty().WithMessage("Symbol is required.");
        }
    }
}