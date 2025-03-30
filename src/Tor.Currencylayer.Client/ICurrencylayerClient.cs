using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client
{
    public interface ICurrencylayerClient
    {
        Task<bool> HealthCheckAsync();

        Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync();

        Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync(string sourceCurrencyCode);

        Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync(string[] destinationCurrencyCodes);

        Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync(string sourceCurrencyCode, string[] destinationCurrencyCodes);
    }
}