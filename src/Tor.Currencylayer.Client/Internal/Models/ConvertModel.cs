using System.Text.Json.Serialization;
using Tor.Currencylayer.Client.Json;

namespace Tor.Currencylayer.Client.Internal.Models
{
    internal class ConvertModel : CurrencylayerModelBase
    {
        [JsonInclude]
        [JsonConverter(typeof(SafeBoolConverter))]
        internal bool Historical { get; set; }

        [JsonInclude]
        internal decimal Result { get; set; }

        [JsonInclude]
        internal DateOnly? Date { get; set; }

        [JsonInclude]
        internal ConvertQueryModel Query { get; set; }

        [JsonInclude]
        internal ConvertInfoModel Info { get; set; }
    }

    internal class ConvertQueryModel
    {
        [JsonInclude]
        internal string From { get; set; }

        [JsonInclude]
        internal string To { get; set; }

        [JsonInclude]
        internal decimal Amount { get; set; }
    }

    internal class ConvertInfoModel
    {
        [JsonInclude]
        internal long Timestamp { get; set; }

        [JsonInclude]
        internal decimal Quote { get; set; }
    }
}
