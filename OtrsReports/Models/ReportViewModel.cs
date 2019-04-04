using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtrsReports.Models
{
    public class ReportViewModel
    {
        public IEnumerable<SelectListItem> Users;
        public DateTime ReportDateFrom { get; set; }
        public DateTime ReportDateTo { get; set; }
        public bool GroupByCompletedUser { get; set; }
    }
}
