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

namespace HomeWorkoutApp.Classes.Workouts
{
    public abstract class Workout
    {
        public const Double LIGHT_VARIANT_REPS_MULTIPLIER = 0.75;
        public const Double LEVEL_1_MULTIPLIER = 1.00;
        public const Double LEVEL_2_MULTIPLIER = 1.25;
        public const Double LEVEL_3_MULTIPLIER = 1.50;
        public const int CURRENT_LEVEL = 3;

        public Workout()
        {
            LogList = new List<Log>();
        }
        public virtual string Title { get; }
        public virtual string Description { get; }
        public virtual string LightVariantDescription { get; }

        public List<Log> LogList { get; set; }
    }
}