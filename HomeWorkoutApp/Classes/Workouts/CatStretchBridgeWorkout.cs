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
    public class CatStretchBridgeWorkout: Workout
    {
        public override string Title => "Catch Stretch Bridge Workout";
        public override string Description => "Position yourself on the floor on all fours.  Ensure that your hips are above your knees then position your hands under your shoulders.  Relax your back and shoulder muscles so that you sink down.  This is your starting position.  Then slowly bridge your back";
        public override string LightVariantDescription => "N/A";
    }
}