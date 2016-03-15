using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A11_RBS.Domain
{
    public class Attendence :Key
    {
        public ApplicationUser User { get; set; }
        public DateTime AttendenceDay { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public bool isLate { get; set; }
        public string Comments { get; set; }
        public string AttendenceMonth { get; set; }
        public string AttendenceYear { get; set; }

    }
}