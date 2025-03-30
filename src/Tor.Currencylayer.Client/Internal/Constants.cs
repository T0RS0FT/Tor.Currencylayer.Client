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
            internal class LatestRates
            {
                internal const string UrlSegment = "live";

                internal class Parameters
                {
                    internal const string SourceCurrencyCode = "source";
                    internal const string CurrencyCodes = "currencies";
                }
            }

            internal class HistoricalRates
            {
                internal const string UrlSegment = "historical";

                internal class Parameters
                {
                    internal const string Date = "date";
                    internal const string SourceCurrencyCode = "source";
                    internal const string CurrencyCodes = "currencies";
                }
            }

            internal class Convert
            {
                internal const string UrlSegment = "convert";

                internal class Parameters
                {
                    internal const string SourceCurrencyCode = "from";
                    internal const string DestinationCurrencyCode = "to";
                    internal const string Amount = "amount";
                    internal const string Date = "date";
                }
            }

            internal class TimeFrame
            {
                internal const string UrlSegment = "timeframe";

                internal class Parameters
                {
                    internal const string StartDate = "start_date";
                    internal const string EndDate = "end_date";
                    internal const string SourceCurrencyCode = "source";
                    internal const string CurrencyCodes = "currencies";
                }
            }

            internal class Change
            {
                internal const string UrlSegment = "change";

                internal class Parameters
                {
                    internal const string StartDate = "start_date";
                    internal const string EndDate = "end_date";
                    internal const string SourceCurrencyCode = "source";
                    internal const string CurrencyCodes = "currencies";
                }
            }
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
