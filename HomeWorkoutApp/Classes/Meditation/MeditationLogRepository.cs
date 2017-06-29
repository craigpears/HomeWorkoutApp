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
    public class MeditationLogRepository
    {
        public IEnumerable<MeditationLog> Get()
        {
            return new List<MeditationLog>();
			// This data was hard coded as it met my needs at the time.  It has been removed for privacy reasons.
        }

        public IEnumerable<MeditationLog> GetForWeek()
        {
            return this.Get().ToList().Where(x => x.DayPerformed > DateTime.Now.AddDays(-7));
        }
    }
}