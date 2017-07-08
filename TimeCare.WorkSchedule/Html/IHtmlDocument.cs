using System.Collections.Generic;

namespace TimeCare.WorkSchedule.Html
{
    public interface IHtmlDocument
    {
        string EmployeeName { get; }

        IEnumerable<WorkShiftRow> WorkShifts { get; }
    }
}