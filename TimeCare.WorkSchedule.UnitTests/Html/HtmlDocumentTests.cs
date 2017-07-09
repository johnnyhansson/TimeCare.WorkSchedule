using System;
using System.IO;
using TimeCare.WorkSchedule.UnitTests.Helpers;
using Xunit;

namespace TimeCare.WorkSchedule.Html.UnitTests
{
    public class HtmlDocumentTests
    {
        [Fact]
        public void ThrowsExceptionIfStreamIsEmpty()
        {
            Stream workScheduleSource = new MemoryStream();

            Assert.Throws<ArgumentOutOfRangeException>(() => new HtmlDocument(workScheduleSource));
        }

        [Fact]
        public void ThrowsExceptionIfStreamIsAtEndPosition()
        {
            Stream workScheduleSource = StreamHelpers.CreateStreamAtEndPosition();

            Assert.Throws<ArgumentOutOfRangeException>(() => new HtmlDocument(workScheduleSource));
        }

        [Fact]
        public void ThrowsExceptionIfWorkScheduleSourceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HtmlDocument(null));
        }
    }
}
