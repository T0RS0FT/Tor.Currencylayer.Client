﻿namespace Tor.Currencylayer.Client.Models
{
    public class HistoricalRatesResult
    {
        public bool Historical { get; set; }

        public string SourceCurrencyCode { get; set; }

        public DateOnly Date { get; set; }

        public long Timestamp { get; set; }

        public List<CurrencyRateResult> Rates { get; set; }
    }
}