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
    public class CircuitLogService
    {
        protected ICircuitLogRepository _circuitLogRepository;

        public CircuitLogService(ICircuitLogRepository circuitLogRepository)
        {
            _circuitLogRepository = circuitLogRepository;
        }

        public List<CircuitLog> SelectRunningWeeksLogsFromWorkouts(DateTime? customReferencePoint = null)
        {
            var allLogs = _circuitLogRepository.Get().ToList();
            DateTime referencePoint = customReferencePoint ?? DateTime.Now;
            DateTime weekBeforeReferencePoint = referencePoint.AddDays(-7);

            WorkoutPeriod currentPeriod;
            if (referencePoint.Hour < 12)
            {
                currentPeriod = WorkoutPeriod.Morning;
            }
            else if (referencePoint.Hour < 17)
            {
                currentPeriod = WorkoutPeriod.Afternoon;
            }
            else
            {
                currentPeriod = WorkoutPeriod.Evening;
            }

            allLogs = allLogs
                .Where(x =>
                       (x.WorkoutDate > weekBeforeReferencePoint && x.WorkoutDate <= referencePoint)
                    || (x.WorkoutDate.DayOfYear == weekBeforeReferencePoint.DayOfYear && x.Period > currentPeriod)
                )
                .OrderByDescending(x => x.WorkoutDate)
                .ThenBy(x => x.Period)
                .ToList();
            return allLogs;
        }
    }
}