using System.Text.Json.Serialization;

namespace Tor.Currencylayer.Client.Internal.Models
{
    internal class CurrencylayerModelBase
    {
        [JsonInclude]
        internal bool Success { get; set; }

        [JsonInclude]
        internal ErrorModel Error { get; set; }
    }
}
