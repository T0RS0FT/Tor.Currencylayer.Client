using System.Text.Json.Serialization;
using Tor.Currencylayer.Client.Enums;
using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client.Internal.Models
{
    internal class ErrorModel
    {
        [JsonInclude]
        internal int Code { get; set; }

        [JsonInclude]
        internal string Info { get; set; }

        public CurrencylayerError ToCurrencylayerError()
        {
            return new CurrencylayerError()
            {
                ErrorType = ErrorType.Currencylayer,
                Code = Code,
                Info = Info,
            };
        }
    }
}
