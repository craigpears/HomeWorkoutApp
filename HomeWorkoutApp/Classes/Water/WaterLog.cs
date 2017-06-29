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

namespace HomeWorkoutApp.Classes.Water
{
    public class WaterLog
    {
        public DateTime Date { get; set; }
        public bool DrankBothWaterBottles { get; set; }
    }
}