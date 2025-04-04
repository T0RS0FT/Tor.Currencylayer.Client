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
                Historical = x.Historical,
                Date = x.Date,
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

        internal static readonly Func<TimeFrameModel, TimeFrameResult> TimeFrames = x =>
            new TimeFrameResult()
            {
                TimeFrame = x.Timeframe,
                SourceCurrencyCode = x.Source,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Items = x.Quotes?.Select(item => new TimeFrameItemResult()
                {
                    Date = item.Key,
                    Rates = item.Value?.Select(rate => new CurrencyRateResult()
                    {
                        CurrencyCode = rate.Key[3..],
                        ExchangeRate = rate.Value
                    }).ToList() ?? []
                }).ToList() ?? []
            };

        internal static readonly Func<ChangeModel, ChangeResult> Change = x =>
            new ChangeResult()
            {
                SourceCurrencyCode = x.Source,
                Change = x.Change,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Rates = x.Quotes?.Select(item => new ChangeRateResult()
                {
                    CurrencyCode = item.Key[3..],
                    Change = item.Value.Change,
                    ChangePercentage = item.Value.ChangePct,
                    StartRate = item.Value.StartRate,
                    EndRate = item.Value.EndRate
                }).ToList() ?? []
            };
    }
}