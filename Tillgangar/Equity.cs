using Newtonsoft.Json;
using System;

public class Equity
{
    public Equity()
    {
    }
    [JsonProperty("Realtime Currency Exchange Rate")]
    public ExchangeRate exchangeRate;




}
public class ExchangeRate
{
    public ExchangeRate()
    {

    }

    [JsonProperty("1. From_Currency Code")]
    public string currencyCodeFrom;
    [JsonProperty("2. From_Currency Name")]
    public string currencyNameFrom;
    [JsonProperty("3. To_Currency Code")]
    public string currencyCodeTo;
    [JsonProperty("4. To_Currency Name")]
    public string currencyNameTo;
    [JsonProperty("5. Exchange Rate")]
    public string exchangeValue;
    [JsonProperty("6. Last Refreshed")]
    public string lastRefreshed;
    [JsonProperty("7. Time Zone")]
    public string timeZone;

}