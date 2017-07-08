using System;
using System.IO;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace TimeCare.WorkSchedule
{
    public class HtmlWorkScheduleReader : IWorkScheduleReader
    {
        private readonly Stream workScheduleSource;

        public HtmlWorkScheduleReader(Stream workScheduleSource)
        {
            if (workScheduleSource == null)
                throw new ArgumentNullException(nameof(workScheduleSource));

            this.workScheduleSource = workScheduleSource;
        }

        public async Task<WorkSchedule> ReadAsync()
        {
            bool streamEmptyOrAtEnd = workScheduleSource.Length == 0 || workScheduleSource.Position == workScheduleSource.Length;

            if (streamEmptyOrAtEnd)
                return null;

            WorkSchedule workSchedule = new WorkSchedule();
            HtmlDocument workScheduleDocument = new HtmlDocument();
            workScheduleDocument.Load(workScheduleSource);

            workSchedule.Employee = workScheduleDocument.DocumentNode.SelectSingleNode("//table[contains(@class, 'Table_Person')]").SelectSingleNode(".//tr[not(@class)]/td").InnerText;
            HtmlNodeCollection workShiftRows = workScheduleDocument.DocumentNode.SelectSingleNode("//table[contains(@class, 'Table_Sched')]").SelectNodes(".//tr[not(@class)]");

            List<WorkShift> workShifts = new List<WorkShift>();

            foreach (var workShiftRow in workShiftRows)
            {
                var workShiftColumns = workShiftRow.Elements("td").ToArray();

                WorkShift workShift = new WorkShift
                {
                    Start = !string.IsNullOrWhiteSpace(workShiftColumns[3].InnerText) ? DateTime.Parse($"{workShiftColumns[1].InnerText} {workShiftColumns[3].InnerText.Substring(0, 2)}:{workShiftColumns[3].InnerText.Substring(2, 2)}") : DateTime.MinValue,
                    End = !string.IsNullOrWhiteSpace(workShiftColumns[4].InnerText) ? DateTime.Parse($"{workShiftColumns[1].InnerText} {workShiftColumns[4].InnerText.Substring(0, 2)}:{workShiftColumns[4].InnerText.Substring(2, 2)}") : DateTime.MinValue,
                    Weekday = workShiftColumns[2].InnerText,
                    WorkCode = workShiftColumns[5].InnerText,
                    Pause = !string.IsNullOrWhiteSpace(workShiftColumns[6].InnerText) ? TimeSpan.Parse($"00:{workShiftColumns[6].InnerText}:00") : TimeSpan.FromMinutes(0),
                    Tasks = workShiftColumns[8].InnerText,
                    Notes = workShiftColumns[9].InnerText
                };

                if (workShift.Duration.Ticks > 0)
                    workShifts.Add(workShift);
            }

            workSchedule.WorkShifts = workShifts;

            return workSchedule;
        }
    }
}
