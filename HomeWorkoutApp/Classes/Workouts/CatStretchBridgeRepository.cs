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
    public class CatStretchBridgeRepository
    {

        public IEnumerable<Log> Get()
        {
            return new List<Log>
            {
                new Log
                {
                     WorkoutDate = new DateTime(2017, 05, 12),
                     Notes = "After 3 minutes or so I started to feel discomfort in the form of pain in my right foot and a burning sensation in my right arm and hand.  Stopped after four minutes.  After some time sitting down I continued to feel some pain on the right side of my back and right foot.",
                     Period = WorkoutPeriod.Afternoon
                },
                new Log
                {
                     WorkoutDate = new DateTime(2017, 05, 12),
                     Notes = "Started with some mild pain in my left foot which increased as I worked out.  Right foot started hurting slightly and towards the end of the workout my right arm started twitching.  Felt some relief and the occasional sensation in my lower back.  Right hand was burning right at the end of the workout.",
                     Period = WorkoutPeriod.Evening
                },
                new Log
                {
                     WorkoutDate = new DateTime(2017, 05, 13),
                     Notes = "Started with some discomfort on the top of my right hand.  This intensified and turned into a burning sensation as the workout progressed.  Got some mild pain in my upper back/shoulders in the latter half of the workout as well, after the workout it feels more on the right hand side.  Tried moving my right hand and the clicking has gone for the first time in months!  Still there a tiny bit must... !!!!  The clicking in my foot seems to have similarly diminished massively.",
                     Period = WorkoutPeriod.Morning
                },
                new Log
                {
                     WorkoutDate = new DateTime(2017, 05, 13),
                     Notes = "Started with some mild upper back pain and discomfort in right hand.  As before, got a burning sensation in my right hand as the workout progressed.  Got some sensation from my left arm and back, a little from my foot maybe but not sure.",
                     Period = WorkoutPeriod.Afternoon
                },
                new Log
                {
                     WorkoutDate = new DateTime(2017, 05, 13),
                     Notes = "Did the workout after going to the cafe and getting a stiff back w/ back pain.  The response from my body was much more mild and I paused less.  Main reaction as usual was burning in my right hand and noticed in the mirror that my face was quite red.  Back felt a lot looser afterwards.",
                     Period = WorkoutPeriod.Afternoon
                },
                new Log
                {
                     WorkoutDate = new DateTime(2017, 05, 14),
                     Notes = "Felt relatively comfortable.  Got the usual burning in my right hand after 2 minutes.  Wrist is starting to feel very loose and minimal cracking now.  Shortly after the workout was stretching out my back and getting some twitching in my right arm.",
                     Period = WorkoutPeriod.Morning
                },
                new Log
                {
                     WorkoutDate = new DateTime(2017, 05, 14),
                     Notes = "Started feeling very uncomfortable after sitting downstairs when parents visited.  Quickly felt strong burning in my left forearm/hand and soon after felt burning in my right hand.  Noticed I was shaking at the end.",
                     Period = WorkoutPeriod.Afternoon
                },
                new Log
                {
                     WorkoutDate = new DateTime(2017, 05, 15),
                     Notes = "Feeling much more comfortable than before.  Usual kind of burning symptoms starting in the left hand and radiating up the arm, right hand burning and right arm trembling.",
                     Period = WorkoutPeriod.Morning
                },
                new Log
                {
                     WorkoutDate = new DateTime(2017, 05, 15),
                     Notes = "Feeling confident after being able to walk for a very short distance out of the house (a few minutes) and being able to stand yesterday.  Walking 10-15 minutes to the shops and back was extremely uncomfortable yesterday but hoping to build up to it.  When working out got the same as before - burning in my left hand and up my left arm.  Later burning in my right and noticed trembling etc. at the end",
                     Period = WorkoutPeriod.Afternoon
                },
                new Log
                {
                     WorkoutDate = new DateTime(2017, 05, 16),
                     Notes = "Feeling similar to previously.  Burning felt a bit more localised to the right side of my right hand at first but progressed to the usual right hand burning, arms shaking etc.",
                     Period = WorkoutPeriod.Morning
                },
                new Log
                {
                     WorkoutDate = new DateTime(2017, 05, 16),
                     Notes = "Had an active morning doing some house chores and then taking some cardboard to the recycling center.  Feeling very tired now and in pain.  Doing the stretches the burns are much more intense, also up my right arm and feet in pain while stretching.",
                     Period = WorkoutPeriod.Afternoon
                }
            };
        }
    }
}