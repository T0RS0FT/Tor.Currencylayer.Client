namespace Tor.Currencylayer.Client.Models
{
    public class CurrencylayerResponse<TResult>
    {
        public bool Success { get; set; }

        public TResult Result { get; set; }

        public CurrencylayerError Error { get; set; }
    }
}
