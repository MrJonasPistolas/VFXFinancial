namespace VFXFinancial.Application.Serialization.Options;
public class SystemTextJsonOptions : IJsonSerializerOptions
{
    public JsonSerializerOptions JsonSerializerOptions { get; } = new();
}