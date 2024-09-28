using Ardalis.GuardClauses;
using VFXFinancial.Application.Exceptions;
using VFXFinancial.Application.Features.Commands.AddEdit;
using VFXFinancial.Domain.Entities;

namespace VFXFinancial.Application.FunctionalTests.ForeignExchangeRates.Commands;

using static Testing;

public class UpdateForeignExchangeRateTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidForeignExchangeRateId()
    {
        var command = new AddEditForeignExchangeRateCommand { Id = 99, FromCurrencyCode = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldUpdateForeignExchangeRate()
    {
        var createCommand = new AddEditForeignExchangeRateCommand
        {
            FromCurrencyCode = "USD",
            FromCurrencyName = "United States Dollar",
            ToCurrencyCode = "EUR",
            ToCurrencyName = "Euro",
            ExchangeRate = 1.019m,
            BidPrice = 1.012m,
            AskPrice = 1.013m
        };

        var resultCreateCommand = await SendAsync(createCommand);

        var updateCommand = new AddEditForeignExchangeRateCommand
        {
            Id = resultCreateCommand.Data,
            FromCurrencyCode = "RON",
            FromCurrencyName = "Romenian Leu",
            ToCurrencyCode = "EUR",
            ToCurrencyName = "Euro",
            ExchangeRate = 4.019m,
            BidPrice = 4.012m,
            AskPrice = 4.013m
        };

        var resultUpdateCommand = await SendAsync(updateCommand);

        var item = await FindAsync<ForeignExchangeRate>(resultUpdateCommand.Data);

        item.Should().NotBeNull();
        item!.FromCurrencyName.Should().Be(updateCommand.FromCurrencyName);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be("System");
    }
}