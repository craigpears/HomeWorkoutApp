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
    public class PointsTests
    {
        public void SetUp()
        {

        }

        [Test]
        public void Should_return_1_point_for_3_rounds_at_current_level()
        {
            var log = new CircuitLog
            {
                Variant = Variant.Normal,
                Level = Workout.CURRENT_LEVEL,
                NumberOfRoundsPerformed = 3
            };

            var points = log.GetPoints();

            Assert.AreEqual(1, points);
        }

        [Test]
        public void Should_multiply_points_at_level_one()
        {
            var log = new CircuitLog
            {
                Variant = Variant.Normal,
                Level = 1,
                NumberOfRoundsPerformed = 3
            };

            var points = log.GetPoints();

            Assert.AreEqual(0.4, points);
        }

        [Test]
        public void Should_half_points_for_light_variants_but_still_apply_level_multiplier()
        {
            var log = new CircuitLog
            {
                Variant = Variant.Light,
                Level = 1,
                NumberOfRoundsPerformed = 3
            };

            var points = log.GetPoints();

            Assert.AreEqual(0.2, points);
        }
    }
}
