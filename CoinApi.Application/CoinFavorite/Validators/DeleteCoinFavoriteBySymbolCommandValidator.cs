using CoinApi.Application.Core.CoinFavorite.Commands;
using FluentValidation;

namespace CoinApi.Application.CoinFavorite.Validators
{
    public class DeleteCoinFavoriteBySymbolCommandValidator : AbstractValidator<DeleteCoinFavoriteBySymbolCommand>
    {
        public DeleteCoinFavoriteBySymbolCommandValidator()
        {
            RuleFor(v => v.Symbol)
                .NotNull().NotEmpty().WithMessage("Symbol is required.");
        }
    }
}