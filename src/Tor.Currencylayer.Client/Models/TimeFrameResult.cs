namespace Tor.Currencylayer.Client.Models
{
    public class TimeFrameResult
    {
        public bool TimeFrame { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string SourceCurrencyCode { get; set; }

        public List<TimeFrameItemResult> Items { get; set; }
    }

    public class TimeFrameItemResult
    {
        public DateOnly Date { get; set; }

        public List<CurrencyRateResult> Rates { get; set; }
    }
}
