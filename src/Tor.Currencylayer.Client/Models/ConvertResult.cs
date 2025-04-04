namespace Tor.Currencylayer.Client.Models
{
    public class ConvertResult
    {
        public decimal Result { get; set; }

        public ConvertQueryResult Query { get; set; }

        public ConvertInfoResult Info { get; set; }
    }

    public class ConvertQueryResult
    {
        public string SourceCurrencyCode { get; set; }

        public string DestinationCurrencyCode { get; set; }

        public decimal Amount { get; set; }
    }

    public class ConvertInfoResult
    {
        public long Timestamp { get; set; }

        public decimal Rate { get; set; }
    }
}
