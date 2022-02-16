using CoinApi.Application.Core.Currency.Commands;
using FluentValidation;

namespace CoinApi.Application.Currency.Validators
{
    public class DeleteAndAddCurrenciesCommandValidator : AbstractValidator<DeleteAndAddCurrenciesCommand>
    {
        public DeleteAndAddCurrenciesCommandValidator()
        {
            RuleFor(v => v.CurrencyDeleteAndAddRequests)
                .NotNull().NotEmpty().WithMessage("Requests is required.");

            RuleForEach(v => v.CurrencyDeleteAndAddRequests).SetValidator(new CurrencyDeleteAndAddRequestValidator());
        }
    }
}