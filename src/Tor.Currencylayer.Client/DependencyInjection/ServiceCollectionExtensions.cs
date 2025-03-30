using Microsoft.Extensions.DependencyInjection;
using Tor.Currencylayer.Client.Internal;

namespace Tor.Currencylayer.Client.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCurrencylayer(this IServiceCollection services, Action<CurrencylayerOptions> currencylayerOptions)
        {
            services.AddScoped<ICurrencylayerClient, CurrencylayerClient>();

            services.AddHttpClient<ICurrencylayerClient, CurrencylayerClient>(options =>
            {
                options.BaseAddress = new Uri(Constants.DefaultCurrencylayerUrl);
            });

            services.Configure(currencylayerOptions);
        }
    }
}
