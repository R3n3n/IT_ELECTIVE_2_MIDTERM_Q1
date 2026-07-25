using IT_ELECTIVE_2_MIDTERM_Q1.Models;

namespace IT_ELECTIVE_2_MIDTERM_Q1.Data
{
    public static class ActivityData
    {
        public static List<PlannerActivity> Activities = new()
        {
            new PlannerActivity
            {
                ActivityDate = new DateTime(2026,8,4),
                ActivityName="Beach Trip",
                PreferredWeather="Clear"
            },

            new PlannerActivity
            {
                ActivityDate = new DateTime(2026,8,10),
                ActivityName="Mountain Hiking",
                PreferredWeather="Clear"
            },

            new PlannerActivity
            {
                ActivityDate = new DateTime(2026,8,15),
                ActivityName="Camping",
                PreferredWeather="Clear"
            },

            new PlannerActivity
            {
                ActivityDate = new DateTime(2026,8,22),
                ActivityName="Cycling",
                PreferredWeather="Clouds"
            },

            new PlannerActivity
            {
                ActivityDate = new DateTime(2026,8,28),
                ActivityName="Nature Walk",
                PreferredWeather="Clouds"
            }
        };
    }
}