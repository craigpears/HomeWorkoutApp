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
using System.Globalization;

namespace HomeWorkoutApp.Classes.Helpers
{
    public class DayOfWeekHelper
    {
        public static String GetAbbreviated(DateTime dateTime)
        {
            return GetAbbreviated(dateTime.DayOfWeek);
        }

        public static String GetAbbreviated(DayOfWeek dayOfWeek)
        {
            CultureInfo english = new CultureInfo("en-GB");
            string[] abbreviatedDayNames = english.DateTimeFormat.AbbreviatedDayNames;
            return abbreviatedDayNames[(int)dayOfWeek];
        }
    }
}