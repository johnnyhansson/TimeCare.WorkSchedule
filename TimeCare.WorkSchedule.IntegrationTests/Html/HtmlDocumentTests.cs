using Shouldly;
using System.IO;
using System.Linq;
using TimeCare.WorkSchedule.IntegrationTests.Helpers;
using Xunit;

namespace TimeCare.WorkSchedule.Html.IntegrationTests
{
    public class HtmlDocumentTests
    {
        private readonly IHtmlDocument document;

        public HtmlDocumentTests()
        {
            Stream workSchedule = StreamHelpers.CreateFromFile(Path.Combine("Resources", "Workschedule.html"));
            document = new HtmlDocument(workSchedule);
        }

        [Fact]
        public void ReadsEmployeeNameFromDocument()
        {
            document.EmployeeName.ShouldBe("John Doe");
        }

        [Fact(Skip = "Needs to be re-implemented")]
        public void ReadsWorkShiftFromDocument()
        {
            document.WorkShifts.ShouldNotBeEmpty();
            document.WorkShifts.Count().ShouldBe(99);
            document.WorkShifts.First().Week.ShouldBe("22");
            document.WorkShifts.First().Date.ShouldBe("2016-05-30");
            document.WorkShifts.First().Weekday.ShouldBe("Måndag");
            document.WorkShifts.First().StartTime.ShouldBe("0700");
            document.WorkShifts.First().EndTime.ShouldBe("1600");
            document.WorkShifts.First().WorkCode.ShouldBe("ARB AN ORT");
            document.WorkShifts.First().PauseDuration.ShouldBe("30");
            document.WorkShifts.First().Duration.ShouldBe("8:30");
            document.WorkShifts.First().Notes.ShouldBeEmpty();
            document.WorkShifts.First().Tasks.ShouldBeEmpty();
            document.WorkShifts.First().TimeBankChanges.ShouldBe("8:30");
            document.WorkShifts.First().BonusTime.ShouldBe("0:00");
        }
    }
}
