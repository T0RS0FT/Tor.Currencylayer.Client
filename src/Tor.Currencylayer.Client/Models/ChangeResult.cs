namespace Tor.Currencylayer.Client.Models
{
    public class ChangeResult
    {
        public string SourceCurrencyCode { get; set; }

        public bool Change { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public List<ChangeRateResult> Rates { get; set; }
    }

    public class ChangeRateResult
    {
        public string CurrencyCode { get; set; }

        public decimal StartRate { get; set; }

        public decimal EndRate { get; set; }

        public decimal Change { get; set; }

        public decimal ChangePercentage { get; set; }
    }
}