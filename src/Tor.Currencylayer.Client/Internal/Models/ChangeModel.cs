using System.Text.Json.Serialization;
using Tor.Currencylayer.Client.Json;

namespace Tor.Currencylayer.Client.Internal.Models
{
    internal class ChangeModel : CurrencylayerModelBase
    {
        [JsonInclude]
        internal string Source { get; set; }

        [JsonInclude]
        [JsonConverter(typeof(SafeBoolConverter))]
        internal bool Change { get; set; }

        [JsonInclude]
        internal DateOnly StartDate { get; set; }

        [JsonInclude]
        internal DateOnly EndDate { get; set; }

        [JsonInclude]
        internal Dictionary<string, ChangeQuoteModel> Quotes { get; set; }
    }

    internal class ChangeQuoteModel
    {
        [JsonInclude]
        internal decimal StartRate { get; set; }

        [JsonInclude]
        internal decimal EndRate { get; set; }

        [JsonInclude]
        internal decimal Change { get; set; }

        [JsonInclude]
        internal decimal ChangePct { get; set; }
    }
}