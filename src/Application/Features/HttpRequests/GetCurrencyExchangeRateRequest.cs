using System.Net;

namespace VFXFinancial.Application.Features.HttpRequests;
public class GetCurrencyExchangeRateRequest : IRequest<Result<RealtimeCurrencyExchangeRateJsonRoot?>>
{
    public string Bid { get; set; }
    public string Ask { get; set; }
}

public class GetCurrencyExchangeRateRequestHandler : IRequestHandler<GetCurrencyExchangeRateRequest, Result<RealtimeCurrencyExchangeRateJsonRoot?>>
{
    private readonly HttpClient _httpClient;
    private readonly ThirdPartyConfiguration thirdPartyConfiguration;

    public GetCurrencyExchangeRateRequestHandler(
        IHttpClientFactory factory,
        IOptions<ThirdPartyConfiguration> options
    )
    {
        _httpClient = factory.CreateClient("api");
        thirdPartyConfiguration = options.Value;
    }

    public async Task<Result<RealtimeCurrencyExchangeRateJsonRoot?>> Handle(GetCurrencyExchangeRateRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/query?function=CURRENCY_EXCHANGE_RATE&from_currency={request.Bid}&to_currency={request.Ask}&apikey={thirdPartyConfiguration.ApiKey}", cancellationToken);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return await Result<RealtimeCurrencyExchangeRateJsonRoot?>.FailAsync("Request Failed");
        }
        var responseAsString = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<RealtimeCurrencyExchangeRateJsonRoot>(responseAsString);
        return await Result<RealtimeCurrencyExchangeRateJsonRoot?>.SuccessAsync(responseObject);
    }
}