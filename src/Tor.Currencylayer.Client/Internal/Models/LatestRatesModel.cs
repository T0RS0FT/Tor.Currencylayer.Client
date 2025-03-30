using System.Text.Json.Serialization;

namespace Tor.Currencylayer.Client.Internal.Models
{
    internal class LatestRatesModel : CurrencylayerModelBase
    {
        [JsonInclude]
        internal long Timestamp { get; set; }

        [JsonInclude]
        internal string Source { get; set; }

        [JsonInclude]
        internal Dictionary<string, decimal> Quotes { get; set; }
    }
}
