using CoinApi.Application.Core.CoinFavorite.Commands;
using FluentValidation;

namespace CoinApi.Application.CoinFavorite.Validators
{
    public class CreateCoinFavoriteCommandValidator : AbstractValidator<CreateCoinFavoriteCommand>
    {
        public CreateCoinFavoriteCommandValidator()
        {
            RuleFor(v => v.Symbol)
                .NotNull().NotEmpty().WithMessage("Symbol is required.");

            RuleFor(v => v.FavoriteCurrencies)
                .NotNull().NotEmpty().WithMessage("Currencies is required.");
        }
    }
}