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
    public class PhysioCircuit : Workout
    {
        public PhysioCircuit()
        {

        }

        public PhysioCircuit(List<CircuitLog> logs)
        {
            this.LogList.AddRange(logs);
        }

        private static int BASE_CRUNCHES_REPS_PER_ROUND = 20;
        private static int BASE_BRIDGES_REPS_PER_ROUND = 12;
        private static int BASE_SQUATS_REPS_PER_ROUND = 12;

        private static int CRUNCHES_REPS_PER_ROUND = (int)(BASE_CRUNCHES_REPS_PER_ROUND * GetLevelMultiplier(CURRENT_LEVEL));
        private static int BRIDGES_REPS_PER_ROUND = (int)(BASE_BRIDGES_REPS_PER_ROUND * GetLevelMultiplier(CURRENT_LEVEL));
        private static int SQUATS_REPS_PER_ROUND = (int)(BASE_SQUATS_REPS_PER_ROUND * GetLevelMultiplier(CURRENT_LEVEL));

        private static int LV_CRUNCHES_REPS_PER_ROUND = (int)(CRUNCHES_REPS_PER_ROUND * LIGHT_VARIANT_REPS_MULTIPLIER);
        private static int LV_BRIDGES_REPS_PER_ROUND = (int)(BRIDGES_REPS_PER_ROUND * LIGHT_VARIANT_REPS_MULTIPLIER);
        private static int LV_SQUATS_REPS_PER_ROUND = (int)(SQUATS_REPS_PER_ROUND * LIGHT_VARIANT_REPS_MULTIPLIER);

        public override string Title => "Physio Circuit";
        public override string Description => $"3 rounds of crunches/bridges/squats, {CRUNCHES_REPS_PER_ROUND}/{BRIDGES_REPS_PER_ROUND}/{SQUATS_REPS_PER_ROUND} reps per round";
        public override string LightVariantDescription => $"2 rounds of {LV_CRUNCHES_REPS_PER_ROUND}/{LV_BRIDGES_REPS_PER_ROUND}/{LV_SQUATS_REPS_PER_ROUND}";

        private IEnumerable<CircuitLog> _normalVariantLogs { get { return LogList.Where(x => x.Variant == Variant.Normal).Select(x => (CircuitLog)x); } }
        private IEnumerable<CircuitLog> _lightVariantLogs { get { return LogList.Where(x => x.Variant == Variant.Light).Select(x => (CircuitLog)x); } }

        public int GetTotalCrunchesPerformed()
        {
            var totalInNormalVariants = (int)_normalVariantLogs.Sum(x => x.NumberOfRoundsPerformed * BASE_CRUNCHES_REPS_PER_ROUND * GetLevelMultiplier(x.Level));
            var totalInLightVariants = (int)_lightVariantLogs.Sum(x => x.NumberOfRoundsPerformed * LV_CRUNCHES_REPS_PER_ROUND * GetLevelMultiplier(x.Level));
            return totalInNormalVariants + totalInLightVariants;
        }

        public int GetTotalBridgesPerformed()
        {
            var totalInNormalVariants = (int)_normalVariantLogs.Sum(x => x.NumberOfRoundsPerformed * BASE_BRIDGES_REPS_PER_ROUND * GetLevelMultiplier(x.Level));
            var totalInLightVariants = (int)_lightVariantLogs.Sum(x => x.NumberOfRoundsPerformed * LV_BRIDGES_REPS_PER_ROUND * GetLevelMultiplier(x.Level));
            return totalInNormalVariants + totalInLightVariants;
        }

        public int GetTotalSquatsPerformed()
        {
            var totalInNormalVariants = (int)_normalVariantLogs.Sum(x => x.NumberOfRoundsPerformed * BASE_SQUATS_REPS_PER_ROUND * GetLevelMultiplier(x.Level));
            var totalInLightVariants = (int)_lightVariantLogs.Sum(x => x.NumberOfRoundsPerformed * LV_SQUATS_REPS_PER_ROUND * GetLevelMultiplier(x.Level));
            return totalInNormalVariants + totalInLightVariants;
        }

        private static double GetLevelMultiplier(int level)
        {
            switch (level)
            {
                case 1:
                    return LEVEL_1_MULTIPLIER;
                case 2:
                    return LEVEL_2_MULTIPLIER;
                case 3:
                    return LEVEL_3_MULTIPLIER;
                default:
                    throw new ArgumentException();
            }
        }
    }
}