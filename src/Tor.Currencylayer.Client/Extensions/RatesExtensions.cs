using Tor.Currencylayer.Client.Interface;
using Tor.Currencylayer.Client.Internal;
using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client.Extensions
{
    public static class RatesExtensions
    {
        public static decimal Convert(
            this IRatesResult rates,
            string sourceCurrencyCode,
            string destinationCurrencyCode,
            decimal quantity)
        {
            ArgumentNullException.ThrowIfNull(rates);
            ArgumentException.ThrowIfNullOrWhiteSpace(sourceCurrencyCode);
            ArgumentException.ThrowIfNullOrWhiteSpace(destinationCurrencyCode);

            if (sourceCurrencyCode.IgnoreCaseEquals(destinationCurrencyCode))
            {
                return quantity;
            }

            if (sourceCurrencyCode.IgnoreCaseEquals(rates.SourceCurrencyCode))
            {
                var rate = rates.Rates.SingleOrDefault(x => x.CurrencyCode.IgnoreCaseEquals(destinationCurrencyCode));

                return rate != null
                    ? rate.ExchangeRate * quantity
                    : throw new Exception(Constants.Messages.DestinationCurrencyCodeNotFound);
            }

            if (destinationCurrencyCode.IgnoreCaseEquals(rates.SourceCurrencyCode))
            {
                var rate = rates.Rates.SingleOrDefault(x => x.CurrencyCode.IgnoreCaseEquals(sourceCurrencyCode));

                return rate != null
                    ? 1 / rate.ExchangeRate * quantity
                    : throw new Exception(Constants.Messages.SourceCurrencyCodeNotFound);
            }

            var sourceRate = rates.Rates.SingleOrDefault(x => x.CurrencyCode.IgnoreCaseEquals(sourceCurrencyCode));
            var destinationRate = rates.Rates.SingleOrDefault(x => x.CurrencyCode.IgnoreCaseEquals(destinationCurrencyCode));

            return sourceRate == null
                ? throw new Exception(Constants.Messages.SourceCurrencyCodeNotFound)
                : destinationRate == null
                    ? throw new Exception(Constants.Messages.DestinationCurrencyCodeNotFound)
                    : destinationRate.ExchangeRate / sourceRate.ExchangeRate * quantity;
        }

        public static LatestRatesResult ChangeBaseCurrency(this LatestRatesResult rates, string baseCurrencyCode)
        {
            ArgumentNullException.ThrowIfNull(rates);
            ArgumentException.ThrowIfNullOrWhiteSpace(baseCurrencyCode);

            return new LatestRatesResult()
            {
                SourceCurrencyCode = baseCurrencyCode.ToUpper(),
                Timestamp = rates.Timestamp,
                Rates = RecalculateRates(rates, baseCurrencyCode)
            };
        }

        public static HistoricalRatesResult ChangeBaseCurrency(this HistoricalRatesResult rates, string baseCurrencyCode)
        {
            ArgumentNullException.ThrowIfNull(rates);
            ArgumentException.ThrowIfNullOrWhiteSpace(baseCurrencyCode);

            return new HistoricalRatesResult()
            {
                Date = rates.Date,
                SourceCurrencyCode = baseCurrencyCode.ToUpper(),
                Timestamp = rates.Timestamp,
                Historical = rates.Historical,
                Rates = RecalculateRates(rates, baseCurrencyCode)
            };
        }

        private static List<CurrencyRateResult> RecalculateRates(IRatesResult rates, string baseCurrencyCode)
        {
            if (rates.SourceCurrencyCode.IgnoreCaseEquals(baseCurrencyCode))
            {
                return rates.Rates.Select(x => new CurrencyRateResult()
                {
                    CurrencyCode = x.CurrencyCode,
                    ExchangeRate = x.ExchangeRate
                }).ToList();
            }

            var baseRate = rates.Rates.SingleOrDefault(x => x.CurrencyCode.IgnoreCaseEquals(baseCurrencyCode))
                ?? throw new Exception(Constants.Messages.CurrencyCodeNotFound);

            return [.. rates.Rates
                .Select(x => x.CurrencyCode.IgnoreCaseEquals(baseCurrencyCode)
                    ? new CurrencyRateResult()
                    {
                        CurrencyCode = rates.SourceCurrencyCode,
                        ExchangeRate = 1 / x.ExchangeRate
                    }
                    : new CurrencyRateResult()
                    {
                        CurrencyCode = x.CurrencyCode,
                        ExchangeRate = x.ExchangeRate / baseRate.ExchangeRate
                    })
                .OrderBy(x => x.CurrencyCode)];
        }
    }
}
