using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using TimeCare.WorkSchedule.Html;

namespace TimeCare.WorkSchedule.UnitTests.Helpers
{
    public static class DocumentHelpers
    {
        public static IHtmlDocument EmptyDocument()
        {
            IHtmlDocument document = Substitute.For<IHtmlDocument>();
            document.EmployeeName.Returns(string.Empty);
            document.WorkShifts.Returns(Enumerable.Empty<WorkShiftRow>());

            return document;
        }

        public static IHtmlDocument DocumentWithDays()
        {
            IHtmlDocument document = Substitute.For<IHtmlDocument>();
            document.EmployeeName.Returns("John Doe");
            document.WorkShifts.Returns(new List<WorkShiftRow>()
            {
                new WorkShiftRow { Date = "2016-05-01", StartTime = "0700", EndTime = "1600" },
                new WorkShiftRow(),
                new WorkShiftRow(),
                new WorkShiftRow { Date = "2016-05-04", StartTime = "0700", EndTime = "1600" },
                new WorkShiftRow { Date = "2016-05-05", StartTime = "0700", EndTime = "1600" },
            });

            return document;
        }
    }
}
