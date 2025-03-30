using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client.BlazorDemo.Extensions
{
    public static class ErrorExtensions
    {
        public static string ToMessage(this CurrencylayerError error)
            => $"Code: {error.Code}, Info: '{error.Info}'";
    }
}
