# Tor.Currencylayer.Client

A C# client library for [Currencylayer.com](https://currencylayer.com) API with dependency injection support.

## Installation

```text
Install-Package Tor.Currencylayer.Client
```

## Usage

### Registering to .NET Core service collection

You have to register the **CurrencylayerClient** with the dependencies in the Program.cs file.

For the minimal registration, you have to add your Currencylayer API key to the options builder:

```text
services.AddCurrencylayer(options =>
{
    options.WithApiKey("Your API key");
});
```

#### Options

Setting the API key:

```text
services.AddCurrencylayer(options =>
{
    options.WithApiKey("Your Currencylayer API key");
});
```

If you want to implement some API key factory logic, f.e.: if you want to change your API key in runtime:

```text
public static class SharedData
{
    public static string ApiKey { get; set; } = "Your Currencylayer API key";
}
```

```text
services.AddCurrencylayer(options =>
{
    options.WithApiKeyFactory(() => SharedData.ApiKey);
});
```

If you use an alias accessing Currencylayer or the Currencylayer base address changes and this package is not updated yet, you can override the base address:

```text
services.AddCurrencylayer(options =>
{
    options.WithBaseUrl("Your URL");
});
```

Based on your design, you can choose a http error handling mode with the following code:

```text
services.AddCurrencylayer(options =>
{
    options.WithHttpErrorHandling(HttpErrorHandlingMode.ReturnsError);
});
```

There are two options (default: ReturnsError):
 - ReturnsError: when the http call ends with an errorcode, the error will be in the response, there will be no exceptions
 - ThrowsException: when http call ends with an errorcode, there will be an exception

Of course, you can combine these options for your needs except **WithApiKey** and **WithApiKeyFactory**.

### ICurrencylayerClient usage

You can get the **ICurrencylayerClient** via dependency injection:

```text
public class MyService
{
    public MyService(ICurrencylayerClient client)
    {
    }   
}
```

> **_NOTE:_**  Please note that depending on your subscription plan, certain API endpoints may or may not be available.

#### Response object

Every method call will return with the following **CurrencylayerResponse<TResult>** class:

```text
public class CurrencylayerResponse<TResult>
{
    public bool Success { get; set; }

    public TResult Result { get; set; }

    public CurrencylayerError Error { get; set; }

    public string Terms { get; set; }

    public string Privacy { get; set; }
}
```

 - When the request succeed
   - Success: true
   - Error: null
   - Result: object
   - Terms: url
   - Privacy: url
 - When the request failed
   - Success: false
   - Error: object
   - Result: null
   - Terms: url / null
   - Privacy: url / null

#### ICurrencylayerClient.GetLatestRatesAsync method

Method parameters:

| Parameter                 | Description                                              | Optional / Required |
| --------------------------|----------------------------------------------------------|---------------------|
| sourceCurrencyCode        | Three letter source currency code                        | Optional            |
| destinationCurrencyCodes  | The codes of the expected result destination currencies  | Optional            |

Response:

| Property               | Description                       |
| ---------------------- | ----------------------------------|
| SourceCurrencyCode     | Three letter source currency code |
| Timestamp              | The UNIX timestamp of the data    |
| Rates                  | List of the exchange rates        |
| Rates -> CurrencyCode  | Three letter currency code        |
| Rates -> ExchangeRate  | Exchange rate                     |

#### ICurrencylayerClient.GetHistoricalRatesAsync method

Method parameters:

| Parameter                 | Description                                                 | Optional / Required |
| --------------------------|-------------------------------------------------------------|---------------------|
| date                      | A date in the past for which historical rates are requested | Required            |
| sourceCurrencyCode        | Three letter source currency code                           | Optional            |
| destinationCurrencyCodes  | The codes of the expected result destination currencies     | Optional            |

Response:

| Property               | Description                       |
| ---------------------- | ----------------------------------|
| Historical             | **true** / **false**              |
| SourceCurrencyCode     | Three letter source currency code |
| Date                   | The date of the data              |
| Timestamp              | The UNIX timestamp of the data    |
| Rates                  | List of the exchange rates        |
| Rates -> CurrencyCode  | Three letter currency code        |
| Rates -> ExchangeRate  | Exchange rate                     |

#### ICurrencylayerClient.ConvertAsync method

Method parameters:

| Parameter                 | Description                                                 | Optional / Required |
| --------------------------|-------------------------------------------------------------|---------------------|
| sourceCurrencyCode        | Three letter source currency code                           | Required            |
| destinationCurrencyCode   | Three letter destination currency code                      | Required            |
| amount                    | Amount to exchange                                          | Required            |
| date                      | A date in the past for which exchange is requested          | Optional            |

Response:

| Property                           | Description                             |
| ---------------------------------- | ----------------------------------------|
| Result                             | The converted amount                    |
| Query                              | Request query info                      |
| Query -> SourceCurrencyCode        | Three letter source currency code       |
| Query -> DestinationCurrencyCode   | Three letter destination currency code  |
| Query -> Amount                    | Amount to exchange                      |
| Info                               | Exchange info                           |
| Info -> Timestamp                  | The UNIX timestamp of the exchange rate |
| Info -> Rate                       | Exchange rate                           |
