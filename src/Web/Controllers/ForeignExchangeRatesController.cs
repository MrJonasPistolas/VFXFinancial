using VFXFinancial.Application.Features.HttpRequests;
using VFXFinancial.Application.Features.Queries.GetByPair;

namespace VFXFinancial.Web.Controllers;

[Route("api/foreign-exchange-rates")]
[ApiController]
public class ForeignExchangeRatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ForeignExchangeRatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateForeignExchangeRate(AddEditForeignExchangeRateCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateForeignExchangeRate(int id, AddEditForeignExchangeRateCommand command)
    {
        if (id != command.Id) return BadRequest();
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteForeignExchangeRate(int id)
    {
        var result = await _mediator.Send(new DeleteForeignExchangeRateCommand { Id = id });
        return Ok(result);
    }

    [HttpGet("{bid}/{ask}")]
    public async Task<IActionResult> GetForeignExchangeRateAsync(string bid, string ask)
    {
        var queryResult = await _mediator.Send(new GetForeignExchangeRateByPairQuery { Ask = ask, Bid = bid });
        if (queryResult.Succeeded)
        {
            return Ok(queryResult);
        }

        var requestResult = await _mediator.Send(new GetCurrencyExchangeRateRequest { Bid = bid, Ask = ask });
        if (requestResult.Succeeded)
        {
            var creationResult = await _mediator.Send(new AddEditForeignExchangeRateCommand
            {
                AskPrice = decimal.Parse(requestResult.Data!.RealtimeCurrencyExchangeRate.AskPrice),
                BidPrice = decimal.Parse(requestResult.Data!.RealtimeCurrencyExchangeRate.BidPrice),
                ExchangeRate = decimal.Parse(requestResult.Data!.RealtimeCurrencyExchangeRate.ExchangeRate),
                FromCurrencyCode = requestResult.Data.RealtimeCurrencyExchangeRate.FromCurrencyCode,
                FromCurrencyName = requestResult.Data.RealtimeCurrencyExchangeRate.FromCurrencyName,
                ToCurrencyCode = requestResult.Data.RealtimeCurrencyExchangeRate.ToCurrencyCode,
                ToCurrencyName = requestResult.Data.RealtimeCurrencyExchangeRate.ToCurrencyName
            });

            if (creationResult.Succeeded)
            {
                return await GetForeignExchangeRateAsync(bid, ask);
            }
        }

        return BadRequest();
    }
}