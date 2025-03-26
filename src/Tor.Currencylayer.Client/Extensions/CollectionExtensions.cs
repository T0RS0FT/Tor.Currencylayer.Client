namespace Tor.Currencylayer.Client.Extensions
{
    internal static class CollectionExtensions
    {
        internal static string ToCurrencylayerCurrencyCodes(this string[] currencyCodes)
            => currencyCodes == null || currencyCodes.Length == 0
                ? string.Empty
                : string.Join(",", currencyCodes).ToUpper();
    }
}
