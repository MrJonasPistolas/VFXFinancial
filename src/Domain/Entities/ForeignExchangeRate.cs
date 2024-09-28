namespace VFXFinancial.Domain.Entities;
public class ForeignExchangeRate : AuditableEntity<int>
{
    public string FromCurrencyCode { get; set; } = string.Empty;
    public string FromCurrencyName { get; set; } = string.Empty;
    public string ToCurrencyCode { get; set; } = string.Empty;
    public string ToCurrencyName { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
    public decimal BidPrice { get; set; }
    public decimal AskPrice { get; set; }
}