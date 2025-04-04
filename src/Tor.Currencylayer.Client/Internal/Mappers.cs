using Tor.Currencylayer.Client.Internal.Models;
using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client.Internal
{
    internal class Mappers
    {
        internal static readonly Func<LatestRatesModel, LatestRatesResult> LatestRates = x =>
            new LatestRatesResult()
            {
                SourceCurrencyCode = x.Source,
                Timestamp = x.Timestamp,
                Rates = x.Quotes?.Select(rate => new CurrencyRateResult()
                {
                    CurrencyCode = rate.Key[3..],
                    ExchangeRate = rate.Value
                }).ToList() ?? []
            };

        internal static readonly Func<HistoricalRatesModel, HistoricalRatesResult> HistoricalRates = x =>
            new HistoricalRatesResult()
            {
                SourceCurrencyCode = x.Source,
                Date = x.Date,
                Historical = x.Historical,
                Timestamp = x.Timestamp,
                Rates = x.Quotes?.Select(rate => new CurrencyRateResult()
                {
                    CurrencyCode = rate.Key[3..],
                    ExchangeRate = rate.Value
                }).ToList() ?? []
            };

        internal static readonly Func<ConvertModel, ConvertResult> Convert = x =>
            new ConvertResult()
            {
                Result = x.Result,
                Query = x.Query == null
                    ? null
                    : new ConvertQueryResult()
                    {
                        SourceCurrencyCode = x.Query.From,
                        DestinationCurrencyCode = x.Query.To,
                        Amount = x.Query.Amount
                    },
                Info = x.Info == null
                    ? null
                    : new ConvertInfoResult()
                    {
                        Timestamp = x.Info.Timestamp,
                        Rate = x.Info.Quote
                    }
            };
    }
}