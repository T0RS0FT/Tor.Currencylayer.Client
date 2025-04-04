using Microsoft.AspNetCore.Components;
using Tor.Currencylayer.Client.BlazorDemo.Extensions;
using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client.BlazorDemo.Pages
{
    public partial class Change
    {
        [Inject]
        private ICurrencylayerClient CurrencylayerClient { get; set; }

        private DateOnly startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-3).Date);
        private DateOnly endDate = DateOnly.FromDateTime(DateTime.Now.Date);
        private string sourceCurrencyCode = string.Empty;
        private string destinationCurrencyCodes = string.Empty;

        private CurrencylayerResponse<ChangeResult> response;
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

            this.response = await CurrencylayerClient.GetChangeAsync(startDate, endDate, sourceCurrencyCode, destinationCodes);

            hasData = this.response.Result != null;
            error = response.Success ? string.Empty : response.Error.ToMessage();
            hasError = !string.IsNullOrWhiteSpace(error);
        }
    }
}
