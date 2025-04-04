﻿using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client
{
    public interface ICurrencylayerClient
    {
        Task<bool> HealthCheckAsync();

        Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync();

        Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync(string sourceCurrencyCode);

        Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync(string[] destinationCurrencyCodes);

        Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync(string sourceCurrencyCode, string[] destinationCurrencyCodes);

        Task<CurrencylayerResponse<HistoricalRatesResult>> GetHistoricalRatesAsync(DateOnly date);

        Task<CurrencylayerResponse<HistoricalRatesResult>> GetHistoricalRatesAsync(DateOnly date, string sourceCurrencyCode);

        Task<CurrencylayerResponse<HistoricalRatesResult>> GetHistoricalRatesAsync(DateOnly date, string[] destinationCurrencyCodes);

        Task<CurrencylayerResponse<HistoricalRatesResult>> GetHistoricalRatesAsync(DateOnly date, string sourceCurrencyCode, string[] destinationCurrencyCodes);

        Task<CurrencylayerResponse<ConvertResult>> ConvertAsync(string sourceCurrencyCode, string destinationCurrencyCode, decimal amount);

        Task<CurrencylayerResponse<ConvertResult>> ConvertAsync(string sourceCurrencyCode, string destinationCurrencyCode, decimal amount, DateOnly? date);
    }
}