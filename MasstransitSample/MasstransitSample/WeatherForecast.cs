namespace MasstransitSample
{
    public class WeatherForecast
    {
        public DateTime Date { get; set;set; }

        public int TemperatureC { get; set;set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set;set; }
    }
}