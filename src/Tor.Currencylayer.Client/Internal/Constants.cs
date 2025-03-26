using System.Text.Json;

namespace Tor.Currencylayer.Client.Internal
{
    internal class Constants
    {
        internal const string DefaultCurrencylayerUrl = "https://api.currencylayer.com/";

        internal const string ApiKeyQueryParamName = "access_key";

        internal static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        internal class Endpoints
        {
        }

        internal class Messages
        {
            internal const string CurrencyCodeNotFound = "Currency code not found";
            internal const string SourceCurrencyCodeNotFound = "Source currency code not found";
            internal const string DestinationCurrencyCodeNotFound = "Destination currency code not found";
            internal const string NotFoundType = "Not found";
            internal const string HttpErrorInfo = "An error occurred during the http request";
        }
    }
}
