using Microsoft.AspNetCore.Components;
using Tor.Currencylayer.Client.BlazorDemo.Extensions;
using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client.BlazorDemo.Pages
{
    public partial class HistoricalRates
    {
        [Inject]
        private ICurrencylayerClient CurrencylayerClient { get; set; }

        private DateOnly date = DateOnly.FromDateTime(DateTime.Now.AddDays(-3));
        private string baseCurrencyCode = string.Empty;
        private string destinationCurrencyCodes = string.Empty;

        private CurrencylayerResponse<HistoricalRatesResult> response;
        private string error = string.Empty;
        private bool hasError = false;
        private bool hasData = false;

        private async Task LoadData()
        {
            if (string.IsNullOrWhiteSpace(Constants.ApiKey))
            {
                this.response = null;
                hasData = false;
                error = "API key required";
                hasError = true;

                return;
            }

            var destinationCodes = destinationCurrencyCodes?
                .Split(",")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToArray() ?? [];

            var response = await CurrencylayerClient.GetHistoricalRatesAsync(date, baseCurrencyCode, destinationCodes);

            this.response = response;
            hasData = this.response.Result != null;
            error = response.Success ? string.Empty : response.Error.ToMessage();
            hasError = !string.IsNullOrWhiteSpace(error);
        }
    }
}
