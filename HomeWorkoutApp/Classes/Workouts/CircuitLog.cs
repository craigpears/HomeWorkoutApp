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
    public class CircuitLog : Log
    {
        public int NumberOfRoundsPerformed { get; set; }
        public override Double GetPoints()
        {
            var fullCircuitsCount = CalculateFullCircuitsCount(out int partialCircuitRoundsCount);
            var partialCircuit = partialCircuitRoundsCount == 2;
            var points = CalculatePoints(fullCircuitsCount, partialCircuit);
            return points;
        }
        
        private int CalculateFullCircuitsCount(out int partialCircuitRoundsCount)
        {
            var roundsInAFullCircuit = 3;
            var fullCircuitsCount = NumberOfRoundsPerformed / roundsInAFullCircuit;
            partialCircuitRoundsCount = NumberOfRoundsPerformed % 3;
            return fullCircuitsCount;
        }

        private Double CalculatePoints(int fullCircuitsCount, bool remainderWasAPartialCircuit)
        {
            var pointsForAFullCircuitAtCurrentLevel = 0.0;
            var pointsForAPartialCircuitAtCurrentLevel = 0.0;

            switch (this.Variant)
            {
                case Variant.Normal:
                    pointsForAFullCircuitAtCurrentLevel = 1.0;
                    pointsForAPartialCircuitAtCurrentLevel = 0.5;
                    break;
                case Variant.Light:
                    pointsForAFullCircuitAtCurrentLevel = 0.5;
                    pointsForAPartialCircuitAtCurrentLevel = 0.0;
                    break;
                default:
                    break;
            }

            ModifyPointsBasedOnWorkoutLevel(ref pointsForAFullCircuitAtCurrentLevel, ref pointsForAPartialCircuitAtCurrentLevel);

            return
                (fullCircuitsCount * pointsForAFullCircuitAtCurrentLevel) +
                (remainderWasAPartialCircuit ? pointsForAPartialCircuitAtCurrentLevel : 0);
        }

        private void ModifyPointsBasedOnWorkoutLevel(ref double pointsForAFullCircuitAtCurrentLevel, ref double pointsForAPartialCircuitAtCurrentLevel)
        {
            var numberOfLevelsBelowCurrentLevel = Workout.CURRENT_LEVEL - this.Level;
            if (numberOfLevelsBelowCurrentLevel == 0) return;
            var pointsModifier = (numberOfLevelsBelowCurrentLevel * Workout.LEVEL_2_MULTIPLIER);
            pointsForAFullCircuitAtCurrentLevel /= pointsModifier;
            pointsForAPartialCircuitAtCurrentLevel /= pointsModifier;
        }
    }
}