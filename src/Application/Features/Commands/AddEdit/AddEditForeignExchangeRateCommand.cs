namespace VFXFinancial.Application.Features.Commands.AddEdit;
public partial class AddEditForeignExchangeRateCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    [Required]
    public string FromCurrencyCode { get; set; } = string.Empty;
    [Required]
    public string FromCurrencyName { get; set; } = string.Empty;
    [Required]
    public string ToCurrencyCode { get; set; } = string.Empty;
    [Required]
    public string ToCurrencyName { get; set; } = string.Empty;
    [Required]
    public decimal ExchangeRate { get; set; }
    [Required]
    public decimal BidPrice { get; set; }
    [Required]
    public decimal AskPrice { get; set; }
}

public class AddEditForeignExchangeRateCommandHandler : IRequestHandler<AddEditForeignExchangeRateCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditForeignExchangeRateCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(AddEditForeignExchangeRateCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var foreignExchangeRate = _mapper.Map<ForeignExchangeRate>(command);
            await _unitOfWork.Repository<ForeignExchangeRate>().AddAsync(foreignExchangeRate);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(foreignExchangeRate.Id, "Foreign Exchange Rate created successfully.");
        }
        else
        {
            var foreignExchangeRate = await _unitOfWork.Repository<ForeignExchangeRate>().GetByIdAsync(command.Id);
            if (foreignExchangeRate is null)
                return await Result<int>.FailAsync("Error trying found the Bid/Ask pair of Foreign Exchange Rate");

            foreignExchangeRate.FromCurrencyCode = command.FromCurrencyCode;
            foreignExchangeRate.FromCurrencyName = command.FromCurrencyName;
            foreignExchangeRate.ToCurrencyCode = command.ToCurrencyCode;
            foreignExchangeRate.ToCurrencyName = command.ToCurrencyName;
            foreignExchangeRate.ExchangeRate = command.ExchangeRate;
            foreignExchangeRate.BidPrice = command.BidPrice;
            foreignExchangeRate.AskPrice = command.AskPrice;

            await _unitOfWork.Repository<ForeignExchangeRate>().UpdateAsync(foreignExchangeRate);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(foreignExchangeRate.Id, "Foreign Exchange Rate updated successfully.");
        }
    }
}