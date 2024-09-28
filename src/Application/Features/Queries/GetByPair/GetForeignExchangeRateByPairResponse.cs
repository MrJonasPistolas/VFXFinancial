namespace VFXFinancial.Application.Features.Queries.GetByPair;
public class GetForeignExchangeRateByPairResponse
{
    public string Bid { get; set; }
    public string Ask { get; set; }
    public decimal ExchangeRate { get; set; }
    public decimal BidPrice { get; set; }
    public decimal AskPrice { get; set; }
}