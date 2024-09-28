namespace VFXFinancial.Application.Serialization.Settings;
public class NewtonsoftJsonSettings : IJsonSerializerSettings
{
    public JsonSerializerSettings JsonSerializerSettings { get; } = new();
}