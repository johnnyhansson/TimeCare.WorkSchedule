using System;
using System.Collections.Generic;

namespace TimeCare.WorkSchedule.Html
{
    public class HtmlWorkScheduleReader : IWorkScheduleReader
    {
        private readonly IHtmlDocument document;

        public HtmlWorkScheduleReader(IHtmlDocument document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            this.document = document;
        }

        public WorkSchedule Read()
        {
            WorkSchedule workSchedule = new WorkSchedule();
            workSchedule.Employee = document.EmployeeName;
            
            List<WorkShift> workShifts = new List<WorkShift>();

            foreach (var workShiftRow in document.WorkShifts)
            {
                WorkShift workShift = new WorkShift
                {
                    Start = !string.IsNullOrWhiteSpace(workShiftRow.StartTime) ? DateTime.Parse($"{workShiftRow.Date} {workShiftRow.StartTime.Substring(0, 2)}:{workShiftRow.StartTime.Substring(2, 2)}") : DateTime.MinValue,
                    End = !string.IsNullOrWhiteSpace(workShiftRow.EndTime) ? DateTime.Parse($"{workShiftRow.Date} {workShiftRow.EndTime.Substring(0, 2)}:{workShiftRow.EndTime.Substring(2, 2)}") : DateTime.MinValue,
                    Weekday = workShiftRow.Weekday,
                    WorkCode = workShiftRow.WorkCode,
                    Pause = !string.IsNullOrWhiteSpace(workShiftRow.PauseDuration) ? TimeSpan.Parse($"00:{workShiftRow.PauseDuration}:00") : TimeSpan.FromMinutes(0),
                    Tasks = workShiftRow.Tasks,
                    Notes = workShiftRow.Notes
                };

                if (workShift.Duration.Ticks > 0)
                    workShifts.Add(workShift);
            }

            workSchedule.WorkShifts = workShifts;

            return workSchedule;
        }
    }
}
