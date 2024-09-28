namespace VFXFinancial.Application.Features.Commands.AddEdit;
public class AddEditForeignExchangeRateCommandValidator : AbstractValidator<AddEditForeignExchangeRateCommand>
{
    public AddEditForeignExchangeRateCommandValidator()
    {
        RuleFor(v => v.FromCurrencyCode)
            .MaximumLength(3)
            .NotEmpty();
        RuleFor(v => v.FromCurrencyName)
            .MaximumLength(30)
            .NotEmpty();
        RuleFor(v => v.ToCurrencyCode)
            .MaximumLength(3)
            .NotEmpty();
        RuleFor(v => v.ToCurrencyName)
            .MaximumLength(30)
            .NotEmpty();
        RuleFor(v => v.ExchangeRate)
            .NotEmpty();
        RuleFor(v => v.BidPrice)
            .NotEmpty();
        RuleFor(v => v.AskPrice)
            .NotEmpty();
    }
}