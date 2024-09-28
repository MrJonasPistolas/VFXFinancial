using FluentAssertions;
using VFXFinancial.Application.Exceptions;
using VFXFinancial.Application.Features.Commands.AddEdit;
using VFXFinancial.Domain.Entities;

namespace VFXFinancial.Application.FunctionalTests.ForeignExchangeRates.Commands;

using static Testing;

public class AddForeignExchangeRateTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new AddEditForeignExchangeRateCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateForeignExchangeRate()
    {
        var command = new AddEditForeignExchangeRateCommand
        {
            FromCurrencyCode = "USD",
            FromCurrencyName = "United States Dollar",
            ToCurrencyCode = "EUR",
            ToCurrencyName = "Euro",
            ExchangeRate = 1.019m,
            BidPrice = 1.012m,
            AskPrice = 1.013m
        };

        var resultCommand = await SendAsync(command);

        var item = await FindAsync<ForeignExchangeRate>(resultCommand.Data);

        item.Should().NotBeNull();
        item.FromCurrencyCode.Should().Be(command.FromCurrencyCode).And.NotBe(command.ToCurrencyCode);
        item.FromCurrencyName.Should().Be(command.FromCurrencyName).And.NotBe(command.ToCurrencyCode);
        item.ToCurrencyCode.Should().Be(command.ToCurrencyCode).And.NotBe(command.FromCurrencyCode);
        item.ToCurrencyName.Should().Be(command.ToCurrencyName).And.NotBe(command.FromCurrencyName); ;
        item.CreatedBy.Should().Be("System");
    }
}