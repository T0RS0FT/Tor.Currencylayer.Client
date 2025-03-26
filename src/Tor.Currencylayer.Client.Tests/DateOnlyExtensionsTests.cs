using Tor.Currencylayer.Client.Extensions;

namespace Tor.Currencylayer.Client.Tests
{
    [TestClass]
    public class DateOnlyExtensionsTests
    {
        [TestMethod]
        public void DateOnlyExtensionsToCurrencylayerFormatTest()
        {
            var date = DateOnly.FromDateTime(DateTime.UtcNow);

            var expected = $"{date.Year:D4}-{date.Month:D2}-{date.Day:D2}";

            Assert.AreEqual(expected, date.ToCurrencylayerFormat());
        }
    }
}
