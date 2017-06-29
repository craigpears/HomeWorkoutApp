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
    public class NotesViewActivity:Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var circuitLogRepository = new CircuitLogRepository();
            var logs = circuitLogRepository.Get().ToList();
            ScrollView scrollView = new ScrollView(this);
            TextView textview = new TextView(this)
            {
                Text = BuildLogDescriptions(logs),
                VerticalScrollBarEnabled = true
            };

            scrollView.AddView(textview);
            SetContentView(scrollView);
        }

        private static string BuildLogDescriptions(List<CircuitLog> logs)
        {
            return logs
                    .OrderByDescending(x => x.WorkoutDate)
                    .ThenByDescending(x => x.Period)
                    .Select(x => $"==={x.WorkoutDate} {x.Period}===\r\n{x.Notes}")
                    .Aggregate((current, next) => current + "\r\n\r\n" + next);
        }
    }
}