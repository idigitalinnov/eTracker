using A11_RBS.Domain;
using A11_RBS.Infrastructure.EnumsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A11_RBS.Domain
{   
    public class Visitors :Key
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string Stream { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }
        public string CurrentCTC { get; set; }
        public string ExpectedJoining { get; set; }
        public string KnowaboutUse { get; set; }


        }
} 