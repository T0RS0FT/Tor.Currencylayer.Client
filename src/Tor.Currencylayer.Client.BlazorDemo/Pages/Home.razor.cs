using Microsoft.AspNetCore.Components;

namespace Tor.Currencylayer.Client.BlazorDemo.Pages
{
    public partial class Home
    {
        [Inject]
        private ICurrencylayerClient CurrencylayerClient { get; set; }

        private string logs = string.Empty;

        private static string ApiKey
        {
            get
            {
                return Constants.ApiKey;
            }
            set
            {
                Constants.ApiKey = value;
            }
        }

        private async Task HealthCheck()
        {
            var result = await CurrencylayerClient.HealthCheckAsync();

            logs += result
                ? $"Service is available{Environment.NewLine}"
                : $"Service is not available{Environment.NewLine}";
        }
    }
}
