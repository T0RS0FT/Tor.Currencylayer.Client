namespace Tor.Currencylayer.Client.Models
{
    public class LatestRatesResult
    {
        public long Timestamp { get; set; }

        public string BaseCurrencyCode { get; set; }

        public List<CurrencyRateResult> Rates { get; set; }
    }
}