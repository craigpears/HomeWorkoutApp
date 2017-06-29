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

namespace HomeWorkoutApp.Classes.Meditation
{
    public class MeditationLog
    {
        public DateTime DayPerformed { get; set; }
        public String MeditationPodcastListenedTo { get; set; }
    }
}