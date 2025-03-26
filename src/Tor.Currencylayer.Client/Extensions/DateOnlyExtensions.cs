namespace Tor.Currencylayer.Client.Extensions
{
    internal static class DateOnlyExtensions
    {
        internal static string ToCurrencylayerFormat(this DateOnly date)
            => date.ToString("O");
    }
}
