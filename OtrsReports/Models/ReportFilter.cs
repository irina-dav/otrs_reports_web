using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtrsReports.Models
{
    public class ReportFilter
    {
        public DateTime ClosedDateFrom { get; set; }
        public DateTime ClosedDateTo { get; set; }
        public List<int> ClosedByUsers { get; set; }
        public bool GroupByCompletedUser { get; set; }
    }
}
