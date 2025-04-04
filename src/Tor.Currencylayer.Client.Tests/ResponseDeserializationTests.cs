using System.Text.Json;
using Tor.Currencylayer.Client.Internal;
using Tor.Currencylayer.Client.Internal.Models;

namespace Tor.Currencylayer.Client.Tests
{
    [TestClass]
    public class ResponseDeserializationTests
    {
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
    }
}
