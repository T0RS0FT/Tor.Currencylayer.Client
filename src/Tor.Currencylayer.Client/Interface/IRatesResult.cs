using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client.Interface
{
    public interface IRatesResult
    {
        public string SourceCurrencyCode { get; set; }

        public List<CurrencyRateResult> Rates { get; set; }
    }
}
