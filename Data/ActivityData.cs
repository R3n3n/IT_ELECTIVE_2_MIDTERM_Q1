using IT_ELECTIVE_2_MIDTERM_Q1.Models;

namespace IT_ELECTIVE_2_MIDTERM_Q1.Data
{
    public static class ActivityData
    {
        public static List<PlannerActivity> Activities = new()
        {
            new PlannerActivity
            {
                ActivityDate = DateTime.Today.AddDays(1),
                ActivityName = "Beach Trip",
                PreferredWeather = "Clear"
            },

            new PlannerActivity
            {
                ActivityDate = DateTime.Today.AddDays(3),
                ActivityName = "Mountain Hiking",
                PreferredWeather = "Clear"
            },

            new PlannerActivity
            {
                ActivityDate = DateTime.Today.AddDays(5),
                ActivityName = "Camping",
                PreferredWeather = "Clear"
            },

            new PlannerActivity
            {
                ActivityDate = DateTime.Today.AddDays(7),
                ActivityName = "Cycling",
                PreferredWeather = "Clouds"
            },

            new PlannerActivity
            {
                ActivityDate = DateTime.Today.AddDays(10),
                ActivityName = "Nature Walk",
                PreferredWeather = "Clouds"
            }
        };
    }
}