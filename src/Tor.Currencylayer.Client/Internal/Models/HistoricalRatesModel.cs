using System.Text.Json.Serialization;
using Tor.Currencylayer.Client.Json;

namespace Tor.Currencylayer.Client.Internal.Models
{
    internal class HistoricalRatesModel : CurrencylayerModelBase
    {
        [JsonInclude]
        internal long Timestamp { get; set; }

        [JsonInclude]
        internal string Source { get; set; }

        [JsonInclude]
        [JsonConverter(typeof(SafeBoolConverter))]
        internal bool Historical { get; set; }

        [JsonInclude]
        internal DateOnly Date { get; set; }

        [JsonInclude]
        internal Dictionary<string, decimal> Quotes { get; set; }
    }
}
