using Ardalis.GuardClauses;
using VFXFinancial.Application.Features.Commands.AddEdit;
using VFXFinancial.Application.Features.Commands.Delete;
using VFXFinancial.Domain.Entities;

namespace VFXFinancial.Application.FunctionalTests.ForeignExchangeRates.Commands;

using static Testing;

public class DeleteForeignExchangeRateTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidForeignExchangeRateId()
    {
        var command = new DeleteForeignExchangeRateCommand { Id = 99 };

        var resultCommand = await SendAsync(command);

        resultCommand.Succeeded.Should().Be(false);
    }

    [Test]
    public async Task ShouldDeleteForeignExchangeRate()
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

        await SendAsync(new DeleteForeignExchangeRateCommand { Id = resultCreateCommand.Data });

        var item = await FindAsync<ForeignExchangeRate>(resultCreateCommand.Data);

        item.Should().BeNull();
    }
}