using A11_RBS.Domain;
using A11_RBS.Infrastructure.EnumsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A11_RBS.Domain
{
    public class UserProfile :Key
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JoiningDate { get; set; }
        public string CurrentAddress { get; set; }
        public EmployeePosition EmployeePosition { get; set; }
        public EmployeeStatus EmployeStatus { get; set; }
        public int LeavesAvailable { get; set; }
        public DateTime CreatedTime { get; set; }
        public UserProfile Manager { get; set; }

    }
}