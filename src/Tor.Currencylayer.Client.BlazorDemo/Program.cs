using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tor.Currencylayer.Client.DependencyInjection;

namespace Tor.Currencylayer.Client.BlazorDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddCurrencylayer(options =>
            {
                options.WithApiKeyFactory(() => Constants.ApiKey);
            });

            await builder.Build().RunAsync();
        }
    }
}
