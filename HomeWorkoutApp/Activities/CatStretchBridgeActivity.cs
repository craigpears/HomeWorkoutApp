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
using HomeWorkoutApp.Classes.Helpers;
using Android.Graphics;

namespace HomeWorkoutApp.Activities
{
    [Activity]
    public class CatStretchBridgeActivity:Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var logRepository = new CatStretchBridgeRepository();
            var logs = logRepository.Get().ToList();
            BuildScrollableLayout(out ScrollView scrollView, out LinearLayout layout);

            foreach (var log in logs)
            {
                AddHeaderText(layout, $"{log.WorkoutDate.DayOfWeek} {log.Period}");
                layout.AddView(new TextView(this) { Text = $"{log.Notes}" });
            }

            SetContentView(scrollView);
        }

        private void AddHeaderText(LinearLayout layout, String text)
        {
            var header = new TextView(this) { Text = text };
            header.SetTypeface(null, TypefaceStyle.Bold);
            layout.AddView(header);
        }

        private void BuildScrollableLayout(out ScrollView scrollView, out LinearLayout layout)
        {
            scrollView = new ScrollView(this);
            layout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            scrollView.AddView(layout);
        }
    }
}