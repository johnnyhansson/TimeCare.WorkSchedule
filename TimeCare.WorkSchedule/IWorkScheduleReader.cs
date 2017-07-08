using System.Threading.Tasks;

namespace TimeCare.WorkSchedule
{
    /// <summary>
    /// Reads a work schedule source and produce a work schedule based on source.
    /// </summary>
    public interface IWorkScheduleReader
    {
        Task<WorkSchedule> ReadAsync();
    }
}
