namespace VFXFinancial.Application.Mappings;
public class ForeignExchangeRateProfile : Profile
{
    public ForeignExchangeRateProfile()
    {
        CreateMap<ForeignExchangeRate, AddEditForeignExchangeRateCommand>();
        CreateMap<AddEditForeignExchangeRateCommand, ForeignExchangeRate>();
    }
}