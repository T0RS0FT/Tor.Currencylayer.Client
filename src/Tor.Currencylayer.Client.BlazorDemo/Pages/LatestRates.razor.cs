using Microsoft.AspNetCore.Components;
using Tor.Currencylayer.Client.BlazorDemo.Extensions;
using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client.BlazorDemo.Pages
{
    public partial class LatestRates
    {
        [Inject]
        private ICurrencylayerClient CurrencylayerClient { get; set; }

        private string baseCurrencyCode = string.Empty;
        private string destinationCurrencyCodes = string.Empty;

        private CurrencylayerResponse<LatestRatesResult> response;
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

            var response = await CurrencylayerClient.GetLatestRatesAsync(baseCurrencyCode, destinationCodes);

            this.response = response;
            hasData = this.response != null;
            error = response.Success ? string.Empty : response.Error.ToMessage();
            hasError = !string.IsNullOrWhiteSpace(error);
        }
    }
}
