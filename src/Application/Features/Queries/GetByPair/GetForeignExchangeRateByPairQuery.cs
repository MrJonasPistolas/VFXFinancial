namespace VFXFinancial.Application.Features.Queries.GetByPair;
public class GetForeignExchangeRateByPairQuery : IRequest<Result<GetForeignExchangeRateByPairResponse?>>
{
    public string Bid { get; set; }
    public string Ask { get; set; }
}

public class GetForeignExchangeRateByPairQueryHandler : IRequestHandler<GetForeignExchangeRateByPairQuery, Result<GetForeignExchangeRateByPairResponse?>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetForeignExchangeRateByPairQueryHandler(
        IUnitOfWork<int> unitOfWork//, 
    )
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetForeignExchangeRateByPairResponse?>> Handle(GetForeignExchangeRateByPairQuery query, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Repository<ForeignExchangeRate>()
            .Entities
            .FirstOrDefaultAsync(x => x.FromCurrencyCode.ToLower() == query.Bid.ToLower() && x.ToCurrencyCode.ToLower() == query.Ask.ToLower());

        if (result is null)
        {
            return await Result<GetForeignExchangeRateByPairResponse?>.FailAsync();
        }

        return await Result<GetForeignExchangeRateByPairResponse?>.SuccessAsync(new GetForeignExchangeRateByPairResponse
        {
            Bid = result.FromCurrencyCode,
            Ask = result.ToCurrencyCode,
            AskPrice = result.AskPrice,
            BidPrice = result.BidPrice,
            ExchangeRate = result.ExchangeRate
        });
    }
}