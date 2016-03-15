using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A11_RBS.Domain
{
    public class SalaryDetails :Key
    {
        public float Salary { get; set; }
        public DateTime SalaryLastRevised { get; set; }
        public DateTime SalaryNextRevision { get; set; }
        public int LastHikePercentage { get; set; }
        public string UserDocumentsPath { get; set; }
        public string FilesUploaded { get; set; }


    }
}