using HomeWorkoutApp.Classes.Workouts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{

    [TestFixture]
    public class RunningWeekTests
    {
        protected Mock<ICircuitLogRepository> _circuitLogRepositoryMock;
        protected CircuitLogService _service;

        [SetUp]
        public void SetUp()
        {
            _circuitLogRepositoryMock = new Mock<ICircuitLogRepository>();
            _circuitLogRepositoryMock.Setup(x => x.Get()).Returns(new List<CircuitLog> {
                new CircuitLog { WorkoutDate = DateTime.Now },
                new CircuitLog { WorkoutDate = DateTime.Now.AddDays(-5) },
                new CircuitLog { WorkoutDate = DateTime.Now.AddDays(-7) },
                new CircuitLog { WorkoutDate = DateTime.Now.AddDays(-8) },
                new CircuitLog { WorkoutDate = DateTime.Now.AddDays(-9) }
            });

            _service = new CircuitLogService(_circuitLogRepositoryMock.Object);
        }

        [Test]
        public void Should_return_data_within_the_last_7_days_by_default()
        {
            var last7DaysData = _service.SelectRunningWeeksLogsFromWorkouts();

            Assert.AreEqual(2, last7DaysData.Count());
        }

        [Test]
        public void Should_return_correct_data_when_reference_point_changes()
        {
            var last7DaysData = _service.SelectRunningWeeksLogsFromWorkouts(DateTime.Now.AddDays(-4));

            Assert.AreEqual(4, last7DaysData.Count());
        }
    }
}
