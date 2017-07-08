using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TimeCare.WorkSchedule.Html
{
    public class HtmlDocument : IHtmlDocument
    {
        private readonly HtmlAgilityPack.HtmlDocument workSchedule;

        public HtmlDocument(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            bool streamEmptyOrAtEnd = stream.Length == 0 || stream.Position == stream.Length;

            if (streamEmptyOrAtEnd)
                throw new ArgumentOutOfRangeException(nameof(stream));

            workSchedule = new HtmlAgilityPack.HtmlDocument();
            workSchedule.Load(stream);
        }

        public string EmployeeName => workSchedule.DocumentNode.SelectSingleNode("//table[contains(@class, 'Table_Person')]").SelectSingleNode(".//tr[not(@class)]/td").InnerText;

        public IEnumerable<WorkShiftRow> WorkShifts
        {
            get
            {
                HtmlNodeCollection workShiftRows = workSchedule.DocumentNode.SelectSingleNode("//table[contains(@class, 'Table_Sched')]").SelectNodes(".//tr[not(@class)]");
                List<WorkShiftRow> workShifts = new List<WorkShiftRow>();

                foreach (var workShiftRow in workShiftRows)
                {
                    var workShiftColumns = workShiftRow.Elements("td").ToArray();

                    WorkShiftRow workShift = new WorkShiftRow
                    {
                        Week = workShiftColumns[0].InnerText,
                        Date = workShiftColumns[1].InnerText,
                        Weekday = workShiftColumns[2].InnerText,
                        StartTime = workShiftColumns[3].InnerText,
                        EndTime = workShiftColumns[4].InnerText,
                        WorkCode = workShiftColumns[5].InnerText,
                        PauseDuration = workShiftColumns[6].InnerText,
                        Duration = workShiftColumns[7].InnerText,
                        Tasks = workShiftColumns[8].InnerText,
                        Notes = workShiftColumns[9].InnerText,
                        TimeBankChanges = workShiftColumns[11].InnerText,
                        BonusTime = workShiftColumns[12].InnerText
                    };

                    workShifts.Add(workShift);
                }

                return workShifts;
            }
        }
    }
}
