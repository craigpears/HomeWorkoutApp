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
    public class Log
    {
        public Log()
        {
            Level = 1;
        }

        public Variant Variant { get; set; }
        public DateTime WorkoutDate { get; set; }
        public WorkoutPeriod Period { get; set; }
        public String Notes { get; set; }
        public int Level { get; set; }
        public virtual Double GetPoints() => throw new NotImplementedException();
    }
}