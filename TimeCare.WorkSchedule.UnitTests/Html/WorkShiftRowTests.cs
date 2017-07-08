using Shouldly;
using TimeCare.WorkSchedule.Html;
using Xunit;

namespace TimeCare.WorkSchedule.UnitTests.Html
{
    public class WorkShiftRowTests
    {
        [Fact]
        public void IsMarkedAsNonScheduledWhenStartTimeIsMissing()
        {
            WorkShiftRow row = new WorkShiftRow();

            row.IsScheduled.ShouldBeFalse();
        }

        [Fact]
        public void IsMarkedAsNonScheduledWhenEndTimeIsMissing()
        {
            WorkShiftRow row = new WorkShiftRow();
            row.StartTime = "0700";

            row.IsScheduled.ShouldBeFalse();
        }
    }
}
