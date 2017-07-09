using System.Linq;
using Shouldly;
using Xunit;
using System.IO;
using TimeCare.WorkSchedule.UnitTests.Helpers;
using System;

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

        [Fact]
        public void ThrowsExceptionIfHtmlDocumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HtmlWorkScheduleReader(null));
        }

        [Fact]
        public void ReturnsAnEmptyWorkScheduleIfHtmlDocumentIsEmpty()
        {
            IWorkScheduleReader reader = new HtmlWorkScheduleReader(DocumentHelpers.EmptyDocument());

            WorkSchedule workSchedule = reader.Read();

            workSchedule.ShouldNotBeNull();
            workSchedule.Employee.ShouldBeEmpty();
            workSchedule.WorkShifts.ShouldBeEmpty();
        }

        [Fact]
        public void OnlyIncludeScheduledDays()
        {
            IWorkScheduleReader reader = new HtmlWorkScheduleReader(DocumentHelpers.DocumentWithDays());

            WorkSchedule workSchedule = reader.Read();

            workSchedule.ShouldNotBeNull();
            workSchedule.Employee.ShouldBe("John Doe");
            workSchedule.WorkShifts.Count().ShouldBe(3);
        }
    }
}
