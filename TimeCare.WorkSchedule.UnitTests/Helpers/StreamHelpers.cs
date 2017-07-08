using System.IO;

namespace TimeCare.WorkSchedule.UnitTests.Helpers
{
    public static class StreamHelpers
    {
        public static Stream CreateStreamAtEndPosition()
        {
            Stream stream = new MemoryStream(new byte[] { 1, 2, 3 });
            stream.Position = stream.Length;

            return stream;
        }

        public static Stream CreateFromFile(string path)
        {
            byte[] content = File.ReadAllBytes(@"Resources\Workschedule.html");
            return new MemoryStream(content);
        }
    }
}
