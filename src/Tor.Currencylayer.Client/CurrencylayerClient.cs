﻿using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using Tor.Currencylayer.Client.Enums;
using Tor.Currencylayer.Client.Extensions;
using Tor.Currencylayer.Client.Internal;
using Tor.Currencylayer.Client.Internal.Models;
using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client
{
    public class CurrencylayerClient(HttpClient httpClient, IOptions<CurrencylayerOptions> options) : ICurrencylayerClient
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly CurrencylayerOptions options = options.Value;

        public async Task<bool> HealthCheckAsync()
        {
            var httpResponse = await httpClient.GetAsync(string.Empty);

            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync()
            => await GetLatestRatesAsync(null, null);

        public async Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync(string sourceCurrencyCode)
            => await GetLatestRatesAsync(sourceCurrencyCode, null);

        public async Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync(string[] destinationCurrencyCodes)
            => await GetLatestRatesAsync(null, destinationCurrencyCodes);

        public async Task<CurrencylayerResponse<LatestRatesResult>> GetLatestRatesAsync(string sourceCurrencyCode, string[] destinationCurrencyCodes)
        {
            var queryParameters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(sourceCurrencyCode))
            {
                queryParameters.Add(
                    Constants.Endpoints.LatestRates.Parameters.SourceCurrencyCode,
                    sourceCurrencyCode);
            }

            if (destinationCurrencyCodes != null && destinationCurrencyCodes.Length > 0)
            {
                queryParameters.Add(
                    Constants.Endpoints.LatestRates.Parameters.CurrencyCodes,
                    destinationCurrencyCodes.ToCurrencylayerCurrencyCodes());
            }

            return await GetResponseAsync(
                Constants.Endpoints.LatestRates.UrlSegment,
                queryParameters,
                Mappers.LatestRates);
        }

        private async Task<CurrencylayerResponse<TResponseModel>> GetResponseAsync<TCurrencylayerModel, TResponseModel>(
            string url,
            Dictionary<string, string> queryParameters,
            Func<TCurrencylayerModel, TResponseModel> mapper)
            where TCurrencylayerModel : CurrencylayerModelBase
        {
            if (!string.IsNullOrWhiteSpace(options.BaseUrl))
            {
                httpClient.BaseAddress = new Uri(options.BaseUrl);
            }

            var httpResponse = await httpClient.GetAsync(GetUrl(url, queryParameters));

            switch (options.HttpErrorHandlingMode)
            {
                case ErrorHandlingMode.ThrowsException:
                    httpResponse.EnsureSuccessStatusCode();
                    break;
                case ErrorHandlingMode.ReturnsError:
                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        return new CurrencylayerResponse<TResponseModel>()
                        {
                            Success = false,
                            Error = new CurrencylayerError()
                            {
                                ErrorType = ErrorType.Http,
                                Code = (int)httpResponse.StatusCode,
                                Info = Constants.Messages.HttpErrorInfo,
                            }
                        };
                    }
                    break;
            }

            var content = await httpResponse.Content.ReadFromJsonAsync<TCurrencylayerModel>(Constants.JsonSerializerOptions);

            return new CurrencylayerResponse<TResponseModel>()
            {
                Success = content.Success,
                Error = content.Error?.ToCurrencylayerError(),
                Result = content.Success ? mapper(content) : default,
                Terms = content.Terms,
                Privacy = content.Privacy
            };
        }

        private string GetApiKey()
        {
            var apiKey = options.ApiKeyFactory?.Invoke() ?? options.ApiKey;

            return !string.IsNullOrWhiteSpace(apiKey)
                ? apiKey
                : throw new Exception("API key required");
        }

        private string GetUrl(string url, Dictionary<string, string> queryParameters)
        {
            queryParameters ??= [];
            if (!queryParameters.ContainsKey(Constants.ApiKeyQueryParamName))
            {
                queryParameters.Add(Constants.ApiKeyQueryParamName, GetApiKey());
            }

            return $"{url}?{string.Join("&", queryParameters.Select(x => $"{x.Key}={x.Value}"))}";
        }
    }
}
