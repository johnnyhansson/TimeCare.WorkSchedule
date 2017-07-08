using System;
using System.Linq;
using Shouldly;
using Xunit;
using System.IO;
using System.Threading.Tasks;
using TimeCare.WorkSchedule.UnitTests.Helpers;

namespace TimeCare.WorkSchedule.Html.UnitTests
{
    public class HtmlWorkScheduleReaderTests
    {
        [Fact]
        public async Task ReturnsNullWhenWorkScheduleSourceIsEmpty()
        {
            Stream workScheduleSource = new MemoryStream();
            IWorkScheduleReader reader = new HtmlWorkScheduleReader(workScheduleSource);

            WorkSchedule workSchedule = await reader.ReadAsync();

            workSchedule.ShouldBeNull();
        }

        [Fact]
        public async Task ReturnsNullIfWorkScheduleSourceIsAtEndPosition()
        {
            Stream workScheduleSource = StreamHelpers.CreateStreamAtEndPosition();
            IWorkScheduleReader reader = new HtmlWorkScheduleReader(workScheduleSource);

            WorkSchedule workSchedule = await reader.ReadAsync();

            workSchedule.ShouldBeNull();
        }

        [Fact]
        public void ThrowsExceptionIfWorkScheduleSourceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HtmlWorkScheduleReader(null));
        }

        [Fact]
        public async Task ReturnsWorkScheduleBasedOnContentFromWorkScheduleSource()
        {
            Stream workScheduleSource = StreamHelpers.CreateFromFile(@"Resources\Workschedule.html");

            IWorkScheduleReader reader = new HtmlWorkScheduleReader(workScheduleSource);

            WorkSchedule actual = await reader.ReadAsync();

            actual.ShouldNotBeNull();
            actual.Employee.ShouldBe("John Doe");
            actual.WorkShifts.Count().ShouldBe(72);
        }
    }
}
