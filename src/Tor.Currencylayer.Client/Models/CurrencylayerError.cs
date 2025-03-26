using Tor.Currencylayer.Client.Enums;

namespace Tor.Currencylayer.Client.Models
{
    public class CurrencylayerError
    {
        public ErrorType ErrorType { get; set; }

        public int Code { get; set; }

        public string Info { get; set; }
    }
}