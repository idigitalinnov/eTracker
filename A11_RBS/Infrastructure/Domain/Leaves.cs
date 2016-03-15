using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using A11_RBS.Infrastructure.EnumsExtensions;

namespace A11_RBS.Domain
{
    public class Leaves : Key
    {
        
        public LeaveType LeaveType { get;set; }
        public string Reason { get; set; }
        public int NoOfDays { get; set; }
        public DateTime RequestedDateTime { get; set;}
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public LeaveApprovalStatus IsApproved { get; set; }
        public DateTime RespondedDateTime { get; set; }
        public string Comments { get; set; }
       

    }
}