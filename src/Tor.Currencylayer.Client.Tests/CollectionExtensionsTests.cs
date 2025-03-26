using Tor.Currencylayer.Client.Extensions;

namespace Tor.Currencylayer.Client.Tests
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [DataTestMethod]
        [DataRow(null, "")]
        [DataRow(new string[] { }, "")]
        [DataRow(new string[] { "EUR" }, "EUR")]
        [DataRow(new string[] { "eur" }, "EUR")]
        [DataRow(new string[] { "EUR", "USD" }, "EUR,USD")]
        [DataRow(new string[] { "EUR", "USD", "GBP" }, "EUR,USD,GBP")]
        [DataRow(new string[] { "EUR", "usd", "GBP" }, "EUR,USD,GBP")]
        public void CollectionExtensionsToCurrencylayerCurrencyCodesTest(string[] currencyCodes, string expectedResult)
        {
            Assert.AreEqual(expectedResult, currencyCodes.ToCurrencylayerCurrencyCodes());
        }
    }
}
