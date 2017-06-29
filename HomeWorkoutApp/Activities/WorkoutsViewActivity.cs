using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HomeWorkoutApp.Classes.Workouts;

namespace HomeWorkoutApp.Activities
{
    [Activity]
    public class WorkoutsViewActivity:Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            TextView textview = new TextView(this)
            {
                Text = BuildWorkoutDescriptions(new List<Workout> { new PhysioCircuit() })
            };
            SetContentView(textview);
        }

        private static string BuildWorkoutDescriptions(List<Workout> workouts)
        {
            return workouts
                    .Select(x => $"==={x.Title}===\r\nDescription: {x.Description} \r\nLight Variant: {x.LightVariantDescription}")
                    .Aggregate((current, next) => current + "\r\n\r\n" + next);
        }
    }
}