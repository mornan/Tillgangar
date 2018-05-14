using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Stocks
{
    public Stocks()
    {
    }
    [JsonProperty("Meta Data")]
    public MetaData metaData { get; set; }
    [JsonProperty("Time Series (Daily)")]
    public Dictionary<string, TimeSeriesJsonClass> Data { get; set; } 


}
public class MetaData
{
    public MetaData()
    {

    }

    [JsonProperty("1. Information")]
    public string information;
    [JsonProperty("2. Symbol")]
    public string symbol;
    [JsonProperty("3. Last Refreshed")]
    public DateTime lastRefreshed;
    [JsonProperty("4. Interval")]
    public string interval;
    [JsonProperty("5. Output Size")]
    public string outputSize;
    [JsonProperty("6. Time Zone")]
    public string timeZone;


}




public class TimeSeriesJsonClass
{
    public TimeSeriesJsonClass()
    { }


    [JsonProperty("1. open")]
    public double open;
    [JsonProperty("2. high")]
    public double high;
    [JsonProperty("3. low")]
    public double low;
    [JsonProperty("4. close")]
    public double close;
    [JsonProperty("5. volume")]
    public double volume;

}