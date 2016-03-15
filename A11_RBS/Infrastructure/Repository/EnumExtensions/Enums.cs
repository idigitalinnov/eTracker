using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A11_RBS.Infrastructure.EnumsExtensions
{

    public enum EmployeeStatus
    {
        Working, Terminated, Absconded, Left
    }

    public enum EmployeePosition
    {
        ASE,
        ProjectCoordinator,
        ProgramManager,
        ProjectDirector,
        TechLead,
        GraphicAnimationDesigner
    }

    /*
    public enum DepartmentName
    {
        AnimationDesign,
        Development,
        Management,
        Marketing
    }*/

    public enum LeaveType
    {
        Sick,
        Causual,
        Planned
    }

    public enum LeaveApprovalStatus
    {
        Approved,
        Pending,
        NotApproved
    }

    public enum ExperienceDetails
    {
        Fresher,
        OnetoThreeYear,
        ThreetoSevenYear,
        SevenPlus

    }
    public enum KnowAboutUs
    {
        Facebook,
        DirectWebsite,
        FriendReferrel,
        SAFEmployee
    }
}