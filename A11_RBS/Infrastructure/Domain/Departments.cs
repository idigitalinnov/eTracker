using A11_RBS.Infrastructure.EnumsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A11_RBS.Domain
{
    public class Departments : Key
    {
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public UserProfile DepartmentHead { get; set; }
        

    }
}
