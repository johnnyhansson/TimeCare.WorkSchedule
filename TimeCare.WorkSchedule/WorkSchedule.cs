using System.Collections.Generic;

namespace TimeCare.WorkSchedule
{
    public class WorkSchedule
    {
        public string Employee { get; set; }

        public IEnumerable<WorkShift> WorkShifts { get; set; }
    }
}
