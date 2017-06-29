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
    public class WaterLogRepository
    {
        public IEnumerable<WaterLog> Get()
        {
            return new List<WaterLog>
            {
                new WaterLog {
                    Date = new DateTime(2017, 5, 3),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 4),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 5),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 6),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 7),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 8),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 9),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 10),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 11),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 12),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 13),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 14),
                    DrankBothWaterBottles = true
                },
                new WaterLog {
                    Date = new DateTime(2017, 5, 15),
                    DrankBothWaterBottles = true
                }
            };
        }



        public IEnumerable<WaterLog> GetForWeek()
        {
            return this.Get().ToList().Where(x => x.Date > DateTime.Now.AddDays(-7));
        }
    }
}