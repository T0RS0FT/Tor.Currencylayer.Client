using System.Text.Json;
using Tor.Currencylayer.Client.Internal;
using Tor.Currencylayer.Client.Internal.Models;

namespace Tor.Currencylayer.Client.Tests
{
    [TestClass]
    public class ResponseDeserializationTests
    {
        [TestMethod]
        public void ErrorDeserializeTest()
        {
            var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "json", "error.json"));

            var model = JsonSerializer.Deserialize<HistoricalRatesModel>(json, Constants.JsonSerializerOptions);

            var error = model.Error?.ToCurrencylayerError();

            Assert.IsNotNull(model);
            Assert.IsFalse(model.Success);
            Assert.IsNotNull(error);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(error.Info));
            Assert.IsTrue(error.Code > 0);
        }

        [TestMethod]
        public void LatestRatesDeserializeTest()
        {
            var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "json", "live.json"));

            var model = JsonSerializer.Deserialize<LatestRatesModel>(json, Constants.JsonSerializerOptions);

            var result = Mappers.LatestRates(model);

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.SourceCurrencyCode));
            Assert.IsTrue(result.Timestamp > 0);
            Assert.IsNotNull(result.Rates);
            Assert.IsTrue(result.Rates.Count > 0);
            Assert.IsTrue(result.Rates.All(x => !string.IsNullOrWhiteSpace(x.CurrencyCode)));
            Assert.IsTrue(result.Rates.All(x => x.ExchangeRate > 0));
            Assert.IsTrue(result.Rates.GroupBy(x => x.CurrencyCode).All(x => x.Count() == 1));
        }

        [TestMethod]
        public void HistoricalRatesDeserializeTest()
        {
            var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "json", "historical.json"));

            var model = JsonSerializer.Deserialize<HistoricalRatesModel>(json, Constants.JsonSerializerOptions);

            var result = Mappers.HistoricalRates(model);

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.SourceCurrencyCode));
            Assert.IsTrue(result.Timestamp > 0);
            Assert.IsTrue(result.Date > DateOnly.MinValue);
            Assert.IsTrue(result.Date < DateOnly.MaxValue);
            Assert.IsTrue(result.Historical);
            Assert.IsNotNull(result.Rates);
            Assert.IsTrue(result.Rates.Count > 0);
            Assert.IsTrue(result.Rates.All(x => !string.IsNullOrWhiteSpace(x.CurrencyCode)));
            Assert.IsTrue(result.Rates.All(x => x.ExchangeRate > 0));
            Assert.IsTrue(result.Rates.GroupBy(x => x.CurrencyCode).All(x => x.Count() == 1));
        }

        [TestMethod]
        public void ConvertDeserializeTest()
        {
            var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "json", "convert.json"));

            var model = JsonSerializer.Deserialize<ConvertModel>(json, Constants.JsonSerializerOptions);

            var result = Mappers.Convert(model);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result > 0);
            Assert.IsNotNull(result.Query);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Query.SourceCurrencyCode));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Query.DestinationCurrencyCode));
            Assert.IsTrue(result.Query.Amount > 0);
            Assert.IsNotNull(result.Info);
            Assert.IsTrue(result.Info.Timestamp > 0);
            Assert.IsTrue(result.Info.Rate > 0);
        }

        [TestMethod]
        public void TimeSeriesDeserializeTest()
        {
            var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "json", "timeframe.json"));

            var model = JsonSerializer.Deserialize<TimeFrameModel>(json, Constants.JsonSerializerOptions);

            var result = Mappers.TimeFrames(model);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TimeFrame);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.SourceCurrencyCode));
            Assert.IsTrue(result.StartDate > DateOnly.MinValue);
            Assert.IsTrue(result.EndDate > DateOnly.MinValue);
            Assert.IsNotNull(result.Items);
            Assert.IsTrue(result.Items.Count > 0);
            result.Items.ForEach(item =>
            {
                Assert.IsTrue(item.Date > DateOnly.MinValue);
                Assert.IsNotNull(item.Rates);
                Assert.IsTrue(item.Rates.Count > 0);
                Assert.IsTrue(item.Rates.All(x => !string.IsNullOrWhiteSpace(x.CurrencyCode)));
                Assert.IsTrue(item.Rates.All(x => x.ExchangeRate > 0));
                Assert.IsTrue(item.Rates.GroupBy(x => x.CurrencyCode).All(x => x.Count() == 1));
            });
        }

        [TestMethod]
        public void ChangeDeserializeTest()
        {
            var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "json", "change.json"));

            var model = JsonSerializer.Deserialize<ChangeModel>(json, Constants.JsonSerializerOptions);

            var result = Mappers.Change(model);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Change);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.SourceCurrencyCode));
            Assert.IsTrue(result.StartDate > DateOnly.MinValue);
            Assert.IsTrue(result.EndDate > DateOnly.MinValue);
            Assert.IsNotNull(result.Rates);
            Assert.IsTrue(result.Rates.Count > 0);
            Assert.IsTrue(result.Rates.All(rate => !string.IsNullOrWhiteSpace(rate.CurrencyCode)));
            Assert.IsTrue(result.Rates.GroupBy(rate => rate.CurrencyCode).All(x => x.Count() == 1));
            Assert.IsTrue(result.Rates.All(rate => rate.StartRate != 0));
            Assert.IsTrue(result.Rates.All(rate => rate.EndRate != 0));
            Assert.IsTrue(result.Rates.All(rate => rate.Change != 0));
            Assert.IsTrue(result.Rates.All(rate => rate.ChangePercentage != 0));
        }
    }
}
