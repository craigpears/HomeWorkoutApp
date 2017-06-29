

using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeWorkoutApp.Classes.Workouts
{
    public class CircuitLogRepository : ICircuitLogRepository
    {
        public CircuitLogRepository()
        {
            
        }

        public IEnumerable<CircuitLog> Get()
        {
            return new List<CircuitLog>();
			// Hard coded data served my purposes at the time, this has been removed for privacy reasons
        }
    }
}