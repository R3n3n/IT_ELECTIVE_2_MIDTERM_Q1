using System.Collections.Generic;

namespace IT_ELECTIVE_2_MIDTERM_Q1.Models
{
    public class WeatherViewModel
    {
        public string City { get; set; }
        public List<DailyForecast> Forecasts { get; set; } = new List<DailyForecast>();
    }

    public class DailyForecast
    {
        public string Date { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public double Humidity { get; set; }
    }
}