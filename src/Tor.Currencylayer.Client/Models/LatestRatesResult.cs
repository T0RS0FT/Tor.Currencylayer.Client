using Tor.Currencylayer.Client.Interface;

namespace Tor.Currencylayer.Client.Models
{
    public class LatestRatesResult : IRatesResult
    {
        public long Timestamp { get; set; }

        public string SourceCurrencyCode { get; set; }

        public List<CurrencyRateResult> Rates { get; set; }
    }
}