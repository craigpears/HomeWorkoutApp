using HomeWorkoutApp.Classes.Workouts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class TotalsTests
    {
        public void SetUp()
        {

        }

        [Test]
        public void Should_not_multiply_reps_by_level_for_level_1()
        {
            var log = new CircuitLog
            {
                Variant = Variant.Normal,
                Level = 1,
                NumberOfRoundsPerformed = 3
            };

            var physioWorkout = new PhysioCircuit();
            physioWorkout.LogList.Add(log);

            var numberOfCrunches = physioWorkout.GetTotalCrunchesPerformed();
            var numberOfBridges = physioWorkout.GetTotalBridgesPerformed();
            var numberOfSquats = physioWorkout.GetTotalSquatsPerformed();

            Assert.AreEqual(60, numberOfCrunches);
            Assert.AreEqual(36, numberOfBridges);
            Assert.AreEqual(36, numberOfSquats);
        }

        [Test]
        public void Should_multiply_reps_by_level_for_level_2()
        {
            var log = new CircuitLog
            {
                Variant = Variant.Normal,
                Level = 2,
                NumberOfRoundsPerformed = 3
            };

            var physioWorkout = new PhysioCircuit();
            physioWorkout.LogList.Add(log);

            var numberOfCrunches = physioWorkout.GetTotalCrunchesPerformed();
            var numberOfBridges = physioWorkout.GetTotalBridgesPerformed();
            var numberOfSquats = physioWorkout.GetTotalSquatsPerformed();

            Assert.AreEqual(75, numberOfCrunches);
            Assert.AreEqual(45, numberOfBridges);
            Assert.AreEqual(45, numberOfSquats);
        }

        [Test]
        public void Should_multiply_reps_by_appropriate_level_for_each_workout_when_there_are_mixed_levels()
        {
            var physioWorkout = new PhysioCircuit();

            physioWorkout.LogList.Add(new CircuitLog
            {
                Variant = Variant.Normal,
                Level = 1,
                NumberOfRoundsPerformed = 3
            });

            physioWorkout.LogList.Add(new CircuitLog
            {
                Variant = Variant.Normal,
                Level = 2,
                NumberOfRoundsPerformed = 3
            });

            physioWorkout.LogList.Add(new CircuitLog
            {
                Variant = Variant.Normal,
                Level = 3,
                NumberOfRoundsPerformed = 3
            });

            var numberOfCrunches = physioWorkout.GetTotalCrunchesPerformed();
            var numberOfBridges = physioWorkout.GetTotalBridgesPerformed();
            var numberOfSquats = physioWorkout.GetTotalSquatsPerformed();

            Assert.AreEqual(225, numberOfCrunches);
            Assert.AreEqual(135, numberOfBridges);
            Assert.AreEqual(135, numberOfSquats);
        }
    }
}
