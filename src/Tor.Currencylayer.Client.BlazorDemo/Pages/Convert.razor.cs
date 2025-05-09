﻿using Microsoft.AspNetCore.Components;
using Tor.Currencylayer.Client.BlazorDemo.Extensions;
using Tor.Currencylayer.Client.Models;

namespace Tor.Currencylayer.Client.BlazorDemo.Pages
{
    public partial class Convert
    {
        [Inject]
        private ICurrencylayerClient CurrencylayerClient { get; set; }

        private string sourceCurrencyCode = string.Empty;
        private string destinationCurrencyCode = string.Empty;
        private decimal amount = 1;
        private DateOnly? date = null;

        private ConvertResult convertResult;
        private string error = string.Empty;
        private bool hasError = false;
        private bool hasData = false;

        private async Task LoadData()
        {
            if (string.IsNullOrWhiteSpace(Constants.ApiKey))
            {
                convertResult = null;
                hasData = false;
                error = "API key required";
                hasError = true;

                return;
            }

            var response = await CurrencylayerClient.ConvertAsync(sourceCurrencyCode, destinationCurrencyCode, amount, date);

            convertResult = response.Result;
            hasData = convertResult != null;
            error = response.Success ? string.Empty : response.Error.ToMessage();
            hasError = !string.IsNullOrWhiteSpace(error);
        }
    }
}
