using System;
using System.Collections.Generic;
using System.Text;

namespace TimeCare.WorkSchedule
{
    public class WorkShift
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Weekday { get; set; }

        public string WorkCode { get; set; }

        public TimeSpan Pause { get; set; }

        public TimeSpan Duration => End.Subtract(Start);

        public string Tasks { get; set; }

        public string Notes { get; set; }
    }
}
