using CoinApi.Application.Core.Coin.Commands;
using FluentValidation;

namespace CoinApi.Application.Coin.Validators
{
    /// <summary>
    /// Delete and add coins validation rules
    /// </summary>
    public class DeleteAndAddCoinsCommandValidator : AbstractValidator<DeleteAndAddCoinsCommand>
    {
        public DeleteAndAddCoinsCommandValidator()
        {
            RuleFor(v => v.CoinCreateRequests)
                .NotNull().NotEmpty().WithMessage("Requests is required.");

            RuleForEach(v => v.CoinCreateRequests).SetValidator(new CoinCreateRequestValidator());
        }
    }
}