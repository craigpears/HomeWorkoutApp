using System.Collections.Generic;

namespace HomeWorkoutApp.Classes.Workouts
{
    public interface ICircuitLogRepository
    {
        IEnumerable<CircuitLog> Get();
    }
}