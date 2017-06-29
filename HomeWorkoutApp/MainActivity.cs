using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Linq;
using HomeWorkoutApp.Classes.Workouts;
using System;
using Android.Graphics;
using Android.Content;
using HomeWorkoutApp.Classes.Meditation;
using HomeWorkoutApp.Activities;
using Android.Views;

namespace HomeWorkoutApp
{
    [Activity(Label = "HomeWorkoutApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : TabActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Main);
            CreateTab(typeof(CatStretchBridgeActivity), "catstretch", "Cat Stretch Bridge", Resource.Drawable.Icon);
            CreateTab(typeof(OverviewViewActivity), "overview", "Overview", Resource.Drawable.Icon);
            CreateTab(typeof(NotesViewActivity), "notes", "Notes", Resource.Drawable.Icon);
            CreateTab(typeof(WorkoutsViewActivity), "workouts", "Workouts", Resource.Drawable.Icon);
        }

        private void CreateTab(Type activityType, string tag, string label, int drawableId)
        {
            var intent = new Intent(this, activityType);
            intent.AddFlags(ActivityFlags.NewTask);

            var spec = TabHost.NewTabSpec(tag);
            var drawableIcon = Resources.GetDrawable(drawableId);
            spec.SetIndicator(label, drawableIcon);
            spec.SetContent(intent);

            TabHost.AddTab(spec);
        }
    }
}

