using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A11_RBS.Domain
{
    public class Timesheet :Key
    {
        public DateTime FillingDate { get; set; }
        public string TimesheetMonth { get; set; }
        public string TimesheetYear { get; set; }
        public string UploadFileName { get; set; }
        public string Status { get; set; }
    }
}