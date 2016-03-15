using A11_RBS.Domain;
using A11_RBS.Infrastructure.EnumsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A11_RBS.Models
{
    public class LeavesModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please Select the Type of Leave")]
        [Display(Name = "Type of Leave")]
        [EnumDataType(typeof(LeaveType))]
        public LeaveType LeaveType { get; set; }

        [Display(Name = "Reason")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Please tell us how many leaves you are requesting for?")]
        [Range(1, 15, ErrorMessage = "Should be Greater than Zero")]
        [Display(Name = "How Many Leaves?")]
        public int NoOfDays { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
           "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please mention the leave starting Date")]
        [Display(Name = "Starting From or One Day")]
        public DateTime FromDate { get; set; }

        [Display(Name = "Leave Till")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
           "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }

        [Display(Name = "Leave Status")]
        [EnumDataType(typeof(LeaveApprovalStatus))]
        public LeaveApprovalStatus IsApproved { get; set; }
        public string Comments { get; set; } 

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
           "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime RequestedDateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
           "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime RespondedDateTime { get; set; }

       

    }
}