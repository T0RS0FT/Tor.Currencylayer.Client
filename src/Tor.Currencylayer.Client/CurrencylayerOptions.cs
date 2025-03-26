using Tor.Currencylayer.Client.Enums;

namespace Tor.Currencylayer.Client
{
    public class CurrencylayerOptions
    {
        public string BaseUrl { get; private set; }

        public string ApiKey { get; private set; }

        public Func<string> ApiKeyFactory { get; private set; }

        public ErrorHandlingMode HttpErrorHandlingMode { get; private set; } = ErrorHandlingMode.ReturnsError;

        public ErrorHandlingMode OtherErrorHandlingMode { get; private set; } = ErrorHandlingMode.ReturnsError;

        public CurrencylayerOptions WithBaseUrl(string baseUrl)
        {
            BaseUrl = baseUrl;

            return this;
        }

        public CurrencylayerOptions WithApiKey(string apiKey)
        {
            if (ApiKeyFactory != null)
            {
                throw new Exception("You can not set the ApiKey and the ApiKeyFactory at the same time");
            }

            ApiKey = apiKey;

            return this;
        }

        public CurrencylayerOptions WithApiKeyFactory(Func<string> apiKeyFactory)
        {
            if (!string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new Exception("You can not set the ApiKey and the ApiKeyFactory at the same time");
            }

            ApiKeyFactory = apiKeyFactory;

            return this;
        }

        public CurrencylayerOptions WithHttpErrorHandling(ErrorHandlingMode httpErrorHandlingMode)
        {
            HttpErrorHandlingMode = httpErrorHandlingMode;

            return this;
        }

        public CurrencylayerOptions WithOtherErrorHandling(ErrorHandlingMode otherErrorHandlingMode)
        {
            OtherErrorHandlingMode = otherErrorHandlingMode;

            return this;
        }
    }
}
