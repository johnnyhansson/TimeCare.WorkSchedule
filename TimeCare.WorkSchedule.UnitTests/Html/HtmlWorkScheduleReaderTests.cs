using System.Linq;
using Shouldly;
using Xunit;
using System.IO;
using TimeCare.WorkSchedule.UnitTests.Helpers;

namespace TimeCare.WorkSchedule.Html.UnitTests
{
    public class HtmlWorkScheduleReaderTests
    {
        [Fact]
        public void ReturnsWorkScheduleBasedOnContentFromWorkScheduleSource()
        {
            Stream workScheduleSource = StreamHelpers.CreateFromFile(@"Resources\Workschedule.html");
            IHtmlDocument document = new HtmlDocument(workScheduleSource);

            IWorkScheduleReader reader = new HtmlWorkScheduleReader(document);

            WorkSchedule actual = reader.Read();

            actual.ShouldNotBeNull();
            actual.Employee.ShouldBe("John Doe");
            actual.WorkShifts.Count().ShouldBe(72);
        }
    }
}
