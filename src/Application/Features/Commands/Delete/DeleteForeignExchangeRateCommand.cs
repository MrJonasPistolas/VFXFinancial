using Ardalis.GuardClauses;

namespace VFXFinancial.Application.Features.Commands.Delete;
public class DeleteForeignExchangeRateCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
}

public class DeleteForeignExchangeRateCommandHandler : IRequestHandler<DeleteForeignExchangeRateCommand, Result<int>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteForeignExchangeRateCommandHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(DeleteForeignExchangeRateCommand command, CancellationToken cancellationToken)
    {
        var foreignExchangeRate = await _unitOfWork.Repository<ForeignExchangeRate>().GetByIdAsync(command.Id);
        if (foreignExchangeRate != null)
        {
            await _unitOfWork.Repository<ForeignExchangeRate>().DeleteAsync(foreignExchangeRate);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(foreignExchangeRate.Id, "Foreign Exchange Rate deleted successfully.");
        }
        else
        {
            return await Result<int>.FailAsync("Error trying found the Bid/Ask pair of Foreign Exchange Rate");
        }
    }
}