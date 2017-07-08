using Shouldly;
using System;
using Xunit;

namespace TimeCare.WorkSchedule.UnitTests
{
    public class WorkShiftTests
    {
        [Fact]
        public void ShouldCalculateDuration()
        {
            TimeSpan expected = new TimeSpan(3, 0, 0);
            WorkShift workShift = new WorkShift
            {
                Start = new DateTime(2016, 4, 15, 14, 0, 0),
                End = new DateTime(2016, 4, 15, 17, 0, 0)
            };

            TimeSpan actual = workShift.Duration;

            actual.ShouldBe(expected);
        }
    }
}
