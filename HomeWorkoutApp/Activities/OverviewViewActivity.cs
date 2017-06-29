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
using HomeWorkoutApp.Classes.Workouts;
using HomeWorkoutApp.Classes.Meditation;
using Android.Graphics;
using HomeWorkoutApp.Classes.Water;
using System.Globalization;
using HomeWorkoutApp.Classes.Helpers;

namespace HomeWorkoutApp.Activities
{
    [Activity]
    public class OverviewViewActivity:Activity
    {
        protected CircuitLogService _circuitLogService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var circuitLogRepository = new CircuitLogRepository();
            var meditationLogRepository = new MeditationLogRepository();
            var waterLogRepository = new WaterLogRepository();
            var physioCircuitWorkout = new PhysioCircuit();
            physioCircuitWorkout.LogList.AddRange(circuitLogRepository.Get());
            _circuitLogService = new CircuitLogService(circuitLogRepository);

            var workouts = new List<Workout>() { physioCircuitWorkout };

            var meditations = meditationLogRepository.GetForWeek().ToList();
            var waterLogs = waterLogRepository.GetForWeek().ToList();
            var layout = new LinearLayout(this) { Orientation = Orientation.Vertical };

            var table = BuildTableLayoutFromWorkouts(workouts, meditations, waterLogs, this);
            layout.AddView(table);

            var reportsHorizontalLayout = new LinearLayout(layout.Context) { Orientation = Orientation.Vertical };
            var currentWeekStatsVerticalLayout = new LinearLayout(layout.Context) { Orientation = Orientation.Vertical };
            var pointsHistoryVerticalLayout = new LinearLayout(layout.Context) { Orientation = Orientation.Horizontal };
            var pointsAggregatesVerticalLayout = new LinearLayout(layout.Context) { Orientation = Orientation.Vertical };
            var currentLevelVerticalLayout = new LinearLayout(layout.Context) { Orientation = Orientation.Vertical };

            layout.AddView(reportsHorizontalLayout);
            reportsHorizontalLayout.AddView(currentWeekStatsVerticalLayout);
            reportsHorizontalLayout.AddView(pointsHistoryVerticalLayout);
            reportsHorizontalLayout.AddView(pointsAggregatesVerticalLayout);
            reportsHorizontalLayout.AddView(currentLevelVerticalLayout);

            currentWeekStatsVerticalLayout.SetPadding(5, 5, 5, 5);
            pointsHistoryVerticalLayout.SetPadding(5, 5, 5, 5);
            pointsAggregatesVerticalLayout.SetPadding(5, 5, 5, 5);

            CreateCurrentWeekStatsSection(workouts, currentWeekStatsVerticalLayout);
            CreateHistorySection(workouts, pointsHistoryVerticalLayout, pointsAggregatesVerticalLayout);

            var currentLevelText = new TextView(this) { Text = "Level " + Workout.CURRENT_LEVEL.ToString() };
            currentLevelText.SetPadding(5, 5, 5, 5);
            currentLevelText.SetTextColor(new Color(255, 255, 255));
            currentLevelVerticalLayout.AddView(currentLevelText);
            
            currentLevelVerticalLayout.AddView
            (
                BuildProgressBarChart
                (
                    steps: new int[] { 12, 15, 22 },
                    goal: 40,
                    title: "Continuous Pushups",
                    icon: Resource.Drawable.Pushups
                )
            );

            var crunchesPerDayData = new List<int>();
            for(int i = 0; i < 7; i++)
            {
                var referenceDate = DateTime.Now.AddDays(-i);
                var logs = _circuitLogService.SelectRunningWeeksLogsFromWorkouts(referenceDate);
                var circuit = new PhysioCircuit(logs);
                var crunchesPerformed = circuit.GetTotalCrunchesPerformed();
                var averagePerDay = crunchesPerformed / 7;
                crunchesPerDayData.Add(averagePerDay);
            }

            currentLevelVerticalLayout.AddView
            (
                BuildProgressBarChart
                (
                    steps: crunchesPerDayData.ToArray(),
                    goal: 200,
                    title: "Crunches per day",
                    icon: Resource.Drawable.Crunches
                )
            );

            currentLevelVerticalLayout.AddView
            (
                BuildProgressBarChart
                (
                    steps: new int[] { 50, 60, 67, 68, 66, 63, 50 },
                    goal: 100,
                    title: "Crunches in a minute",
                    icon: Resource.Drawable.Crunches
                )
            );

            SetContentView(layout);
        }

        private LinearLayout BuildProgressBarChart(int[] steps, int goal, string title, int icon)
        {
            var outerWrapper = new LinearLayout(this) { Orientation = Orientation.Vertical };
            var barChart = new LinearLayout(this);
            var header = new LinearLayout(this);
            outerWrapper.AddView(header);
            outerWrapper.AddView(barChart);

            header.AddView(new TextView(this) { Text = $" {title}" });
            header.AddView(GetIconImage(icon));
            
            var minPoints = steps[0];
            var allSteps = steps.ToList();
            allSteps.Add(goal);
            foreach (var step in allSteps)
            {
                var textView = new TextView(this) { Text = $"{step}" };
                var heightOfGoal = 150.0;
                var pixelsPerPoint = heightOfGoal / goal;
                var heightOfThisElement = (int)(pixelsPerPoint * step);

                textView.SetHeight(heightOfThisElement);
                textView.SetBackgroundColor(new Color(255, 255, 255));
                textView.SetTextColor(new Color(0, 0, 0));
                textView.SetPadding(5, 5, 5, 5);
                textView.Gravity = GravityFlags.Bottom;
                textView.TextSize *= (float)0.5;
                barChart.AddView(textView);

                var extraTextToForceAMargin = new TextView(this);
                extraTextToForceAMargin.SetWidth(5);
                barChart.AddView(extraTextToForceAMargin);
            }
            return outerWrapper;
        }

        private ImageView GetIconImage(int icon)
        {
            var iconImage = new ImageView(this);
            iconImage.SetImageResource(icon);
            return iconImage;
        }

        private void CreateHistorySection(List<Workout> workouts, LinearLayout pointsHistoryVerticalLayout, LinearLayout pointsAggregatesVerticalLayout)
        {
            var pointsToday = _circuitLogService.SelectRunningWeeksLogsFromWorkouts().Sum(x => x.GetPoints());
            var pointsYesterday = _circuitLogService.SelectRunningWeeksLogsFromWorkouts(customReferencePoint: DateTime.Now.AddDays(-1)).Sum(x => x.GetPoints());
            var pointsTwoDaysAgo = _circuitLogService.SelectRunningWeeksLogsFromWorkouts(customReferencePoint: DateTime.Now.AddDays(-2)).Sum(x => x.GetPoints());
            var pointsThreeDaysAgo = _circuitLogService.SelectRunningWeeksLogsFromWorkouts(customReferencePoint: DateTime.Now.AddDays(-3)).Sum(x => x.GetPoints());
            var pointsFourDaysAgo = _circuitLogService.SelectRunningWeeksLogsFromWorkouts(customReferencePoint: DateTime.Now.AddDays(-4)).Sum(x => x.GetPoints());
            var pointsFiveDaysAgo = _circuitLogService.SelectRunningWeeksLogsFromWorkouts(customReferencePoint: DateTime.Now.AddDays(-5)).Sum(x => x.GetPoints());
            var pointsSixDaysAgo = _circuitLogService.SelectRunningWeeksLogsFromWorkouts(customReferencePoint: DateTime.Now.AddDays(-6)).Sum(x => x.GetPoints());
            var pointsAWeekAgo = _circuitLogService.SelectRunningWeeksLogsFromWorkouts(customReferencePoint: DateTime.Now.AddDays(-7)).Sum(x => x.GetPoints());

            var listOfAverages = new List<double> { pointsToday, pointsYesterday, pointsTwoDaysAgo, pointsThreeDaysAgo, pointsFourDaysAgo, pointsFiveDaysAgo, pointsSixDaysAgo, pointsAWeekAgo };
            var best = listOfAverages.Max();
            var min = listOfAverages.Min();
            var avg = listOfAverages.Average();

            AddPointsHistoryForDay(pointsHistoryVerticalLayout, pointsAWeekAgo, best, min);
            AddPointsHistoryForDay(pointsHistoryVerticalLayout, pointsSixDaysAgo, best, min);
            AddPointsHistoryForDay(pointsHistoryVerticalLayout, pointsFiveDaysAgo, best, min);
            AddPointsHistoryForDay(pointsHistoryVerticalLayout, pointsFourDaysAgo, best, min);
            AddPointsHistoryForDay(pointsHistoryVerticalLayout, pointsThreeDaysAgo, best, min);
            AddPointsHistoryForDay(pointsHistoryVerticalLayout, pointsTwoDaysAgo, best, min);
            AddPointsHistoryForDay(pointsHistoryVerticalLayout, pointsYesterday, best, min);
            AddPointsHistoryForDay(pointsHistoryVerticalLayout, pointsToday, best, min);
            
            pointsAggregatesVerticalLayout.AddView(new TextView(pointsAggregatesVerticalLayout.Context) { Text = $"Average: {avg}" });
        }

        private static void AddPointsHistoryForDay(LinearLayout pointsHistoryVerticalLayout, double points, double maxPoints, double minPoints)
        {
            var textView = new TextView(pointsHistoryVerticalLayout.Context) { Text = $"{points}" };
            var heightOfMax = 200;
            var pixelsPerPoint = (double)heightOfMax / maxPoints;

            var pointsOverTheMinimum = points - minPoints;
            var heightOfThisElement = (int)(pixelsPerPoint * points);
            
            textView.SetHeight(heightOfThisElement);
            textView.SetBackgroundColor(new Color(255, 255, 255));
            textView.SetTextColor(new Color(0, 0, 0));
            textView.SetPadding(10, 10, 10, 10);
            textView.Gravity = GravityFlags.Bottom;
            pointsHistoryVerticalLayout.AddView(textView);

            var extraTextToForceAMargin = new TextView(pointsHistoryVerticalLayout.Context);
            extraTextToForceAMargin.SetWidth(5);
            pointsHistoryVerticalLayout.AddView(extraTextToForceAMargin);
        }

        private void CreateCurrentWeekStatsSection(List<Workout> workouts, LinearLayout currentWeekStatsLayout)
        {
            var logsForWeek = _circuitLogService.SelectRunningWeeksLogsFromWorkouts();

            var physioCircuitWorkout = new PhysioCircuit ();
            physioCircuitWorkout.LogList.AddRange(logsForWeek);
            var totalBridgesForWeek = physioCircuitWorkout.GetTotalBridgesPerformed();
            var totalSquatsForWeek = physioCircuitWorkout.GetTotalSquatsPerformed();
            var averageBridgesPerDay = totalBridgesForWeek / 7;
            var averageSquatsPerDay = totalSquatsForWeek / 7;

            var orderedListOfPointsForWeek = logsForWeek
                .GroupBy(x => x.WorkoutDate.DayOfWeek)
                .Select(x => x.Sum(y => y.GetPoints()))
                .OrderBy(x => x).ToList();
            var daysWithWorkoutsCount = orderedListOfPointsForWeek.Count();
            var daysWithoutWorkoutsCount = 7 - daysWithWorkoutsCount;
            
            currentWeekStatsLayout.AddView(new TextView(currentWeekStatsLayout.Context) { Text = $"{totalBridgesForWeek} bridges this week! {averageBridgesPerDay} on average per day!" });
            currentWeekStatsLayout.AddView(new TextView(currentWeekStatsLayout.Context) { Text = $"{totalSquatsForWeek} squats this week! {averageSquatsPerDay} on average per day!" });
        }

        private TableLayout BuildTableLayoutFromWorkouts(List<Workout> workouts, List<MeditationLog> meditations, List<WaterLog> waterLogs, Context context)
        {
            var allLogs = _circuitLogService.SelectRunningWeeksLogsFromWorkouts().Select(x => (Log)x).ToList();

            var table = new TableLayout(context);
            var headerRow = new TableRow(context);
            var morningRow = new TableRow(context);
            var afternoonRow = new TableRow(context);
            var eveningRow = new TableRow(context);
            var iconsRow = new TableRow(context);
            var waterRow = new TableRow(context);

            table.AddView(headerRow);
            table.AddView(morningRow);
            table.AddView(afternoonRow);
            table.AddView(eveningRow);
            table.AddView(iconsRow);
            table.AddView(waterRow);

            AddDayOfWeekToHeaderRow(context, headerRow, DayOfWeek.Monday);
            AddDayOfWeekToHeaderRow(context, headerRow, DayOfWeek.Tuesday);
            AddDayOfWeekToHeaderRow(context, headerRow, DayOfWeek.Wednesday);
            AddDayOfWeekToHeaderRow(context, headerRow, DayOfWeek.Thursday);
            AddDayOfWeekToHeaderRow(context, headerRow, DayOfWeek.Friday);
            AddDayOfWeekToHeaderRow(context, headerRow, DayOfWeek.Saturday);
            AddDayOfWeekToHeaderRow(context, headerRow, DayOfWeek.Sunday);

            AddLogByDayAndPeriod(allLogs, DayOfWeek.Monday, WorkoutPeriod.Morning, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Monday, WorkoutPeriod.Afternoon, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Monday, WorkoutPeriod.Evening, context, morningRow, afternoonRow, eveningRow);

            AddLogByDayAndPeriod(allLogs, DayOfWeek.Tuesday, WorkoutPeriod.Morning, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Tuesday, WorkoutPeriod.Afternoon, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Tuesday, WorkoutPeriod.Evening, context, morningRow, afternoonRow, eveningRow);

            AddLogByDayAndPeriod(allLogs, DayOfWeek.Wednesday, WorkoutPeriod.Morning, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Wednesday, WorkoutPeriod.Afternoon, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Wednesday, WorkoutPeriod.Evening, context, morningRow, afternoonRow, eveningRow);

            AddLogByDayAndPeriod(allLogs, DayOfWeek.Thursday, WorkoutPeriod.Morning, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Thursday, WorkoutPeriod.Afternoon, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Thursday, WorkoutPeriod.Evening, context, morningRow, afternoonRow, eveningRow);

            AddLogByDayAndPeriod(allLogs, DayOfWeek.Friday, WorkoutPeriod.Morning, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Friday, WorkoutPeriod.Afternoon, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Friday, WorkoutPeriod.Evening, context, morningRow, afternoonRow, eveningRow);

            AddLogByDayAndPeriod(allLogs, DayOfWeek.Saturday, WorkoutPeriod.Morning, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Saturday, WorkoutPeriod.Afternoon, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Saturday, WorkoutPeriod.Evening, context, morningRow, afternoonRow, eveningRow);

            AddLogByDayAndPeriod(allLogs, DayOfWeek.Sunday, WorkoutPeriod.Morning, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Sunday, WorkoutPeriod.Afternoon, context, morningRow, afternoonRow, eveningRow);
            AddLogByDayAndPeriod(allLogs, DayOfWeek.Sunday, WorkoutPeriod.Evening, context, morningRow, afternoonRow, eveningRow);

            AddIconsForDay(meditations, waterLogs, DayOfWeek.Monday, context, iconsRow);
            AddIconsForDay(meditations, waterLogs, DayOfWeek.Tuesday, context, iconsRow);
            AddIconsForDay(meditations, waterLogs, DayOfWeek.Wednesday, context, iconsRow);
            AddIconsForDay(meditations, waterLogs, DayOfWeek.Thursday, context, iconsRow);
            AddIconsForDay(meditations, waterLogs, DayOfWeek.Friday, context, iconsRow);
            AddIconsForDay(meditations, waterLogs, DayOfWeek.Saturday, context, iconsRow);
            AddIconsForDay(meditations, waterLogs, DayOfWeek.Sunday, context, iconsRow);

            return table;
        }

        private static void AddIconsForDay(List<MeditationLog> meditations, List<WaterLog> waterLogs, DayOfWeek dayOfWeek, Context context, TableRow iconsRow)
        {
            var layout = new LinearLayout(context)
            {
                Orientation = Orientation.Horizontal
            };
            iconsRow.SetPadding(10, 10, 10, 10);
            iconsRow.AddView(layout);
            if (meditations.Any(x => x.DayPerformed.DayOfWeek == dayOfWeek))
            {
                var imageView = new ImageView(context);
                imageView.SetImageResource(Resource.Drawable.Meditation);
                layout.AddView(imageView);
            }

            if (waterLogs.Any(x => x.Date.DayOfWeek == dayOfWeek && x.DrankBothWaterBottles))
            {
                var imageView = new ImageView(context);
                imageView.SetImageResource(Resource.Drawable.Water);
                layout.AddView(imageView);
            }
        }

        private static void AddLogByDayAndPeriod(List<Log> allLogs,
            DayOfWeek dayOfWeek,
            WorkoutPeriod workoutPeriod,
            Context context,
            TableRow morningRow,
            TableRow afternoonRow,
            TableRow eveningRow)
        {
            var log = allLogs.SingleOrDefault(x => x.WorkoutDate.DayOfWeek == dayOfWeek && x.Period == workoutPeriod);

            TableRow tableRow = null;
            switch (workoutPeriod)
            {
                case WorkoutPeriod.Morning:
                    tableRow = morningRow;
                    break;
                case WorkoutPeriod.Afternoon:
                    tableRow = afternoonRow;
                    break;
                case WorkoutPeriod.Evening:
                    tableRow = eveningRow;
                    break;
                default:
                    throw new ArgumentException("Unrecognised workout period - AddLogByDayAndPeriod");
            }

            AddLogToTable(context, tableRow, log, dayOfWeek);
        }

        private static void AddDayOfWeekToHeaderRow(Context context, TableRow headerRow, DayOfWeek dayOfWeek)
        {
            var dayTextView = new TextView(context) { Text = DayOfWeekHelper.GetAbbreviated(dayOfWeek) };
            dayTextView.SetTypeface(null, TypefaceStyle.Bold);
            dayTextView.SetPadding(5, 5, 5, 5);
            headerRow.AddView(dayTextView);
        }

        private static void AddLogToTable(Context context, TableRow tableRow, Log log, DayOfWeek dayOfWeek)
        {
            var logTextView = new TextView(context);
            logTextView.SetPadding(10, 10, 10, 10);
            logTextView.SetTextColor(new Color(255, 255, 255));
            if (dayOfWeek == DateTime.Now.DayOfWeek)
            {
                logTextView.SetBackgroundColor(new Color(173, 216, 230));
                logTextView.SetTextColor(new Color(0, 0, 0));
            }
            tableRow.AddView(logTextView);
            if (log != null)
            {
                logTextView.Text = log.GetPoints().ToString();
            }
            else
            {
                logTextView.Text = "X";
                logTextView.SetTypeface(null, TypefaceStyle.Bold);
            }
        }
    }
}