namespace Tor.Currencylayer.Client
{
    public interface ICurrencylayerClient
    {
        Task<bool> HealthCheckAsync();
    }
}
