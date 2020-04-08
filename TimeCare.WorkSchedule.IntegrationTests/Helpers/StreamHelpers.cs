using System.IO;

namespace TimeCare.WorkSchedule.IntegrationTests.Helpers
{
    public static class StreamHelpers
    {
        public static Stream CreateFromFile(string path)
        {
            byte[] content = File.ReadAllBytes(path);
            return new MemoryStream(content);
        }
    }
}
