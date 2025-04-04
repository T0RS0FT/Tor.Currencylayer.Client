using System.Text.Json.Serialization;
using Tor.Currencylayer.Client.Json;

namespace Tor.Currencylayer.Client.Internal.Models
{
    internal class TimeFrameModel : CurrencylayerModelBase
    {
        [JsonInclude]
        [JsonConverter(typeof(SafeBoolConverter))]
        internal bool Timeframe { get; set; }

        [JsonInclude]
        internal DateOnly StartDate { get; set; }

        [JsonInclude]
        internal DateOnly EndDate { get; set; }

        [JsonInclude]
        internal string Source { get; set; }

        [JsonInclude]
        internal Dictionary<DateOnly, Dictionary<string, decimal>> Quotes { get; set; }
    }
}
