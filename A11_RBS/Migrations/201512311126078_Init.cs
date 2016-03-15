namespace A11_RBS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AttendenceDay = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LoginTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LogoutTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isLate = c.Boolean(nullable: false),
                        Comments = c.String(),
                        AttendenceMonth = c.String(),
                        AttendenceYear = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Department_Id = c.Guid(),
                        Salarydetails_Id = c.Guid(),
                        Userprofile_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .ForeignKey("dbo.SalaryDetails", t => t.Salarydetails_Id)
                .ForeignKey("dbo.UserProfiles", t => t.Userprofile_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Department_Id)
                .Index(t => t.Salarydetails_Id)
                .Index(t => t.Userprofile_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DepartmentName = c.String(),
                        Description = c.String(),
                        DepartmentHead_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.DepartmentHead_Id)
                .Index(t => t.DepartmentHead_Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        BirthDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        JoiningDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CurrentAddress = c.String(),
                        EmployeePosition = c.Int(nullable: false),
                        EmployeStatus = c.Int(nullable: false),
                        LeavesAvailable = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Manager_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.Manager_Id)
                .Index(t => t.Manager_Id);
            
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LeaveType = c.Int(nullable: false),
                        Reason = c.String(),
                        NoOfDays = c.Int(nullable: false),
                        RequestedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FromDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ToDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsApproved = c.Int(nullable: false),
                        RespondedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Comments = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SalaryDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Salary = c.Single(nullable: false),
                        SalaryLastRevised = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SalaryNextRevision = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastHikePercentage = c.Int(nullable: false),
                        UserDocumentsPath = c.String(),
                        FilesUploaded = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Timesheets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FillingDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TimesheetMonth = c.String(),
                        TimesheetYear = c.String(),
                        UploadFileName = c.String(),
                        Status = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        FatherName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Qualification = c.String(),
                        Stream = c.String(),
                        Skills = c.String(),
                        Experience = c.String(),
                        CurrentCTC = c.String(),
                        ExpectedJoining = c.String(),
                        KnowaboutUse = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Attendences", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Userprofile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.Timesheets", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Salarydetails_Id", "dbo.SalaryDetails");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Leaves", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Departments", "DepartmentHead_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.UserProfiles", "Manager_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Timesheets", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Leaves", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserProfiles", new[] { "Manager_Id" });
            DropIndex("dbo.Departments", new[] { "DepartmentHead_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Userprofile_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Salarydetails_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Department_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Attendences", new[] { "User_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Visitors");
            DropTable("dbo.Timesheets");
            DropTable("dbo.SalaryDetails");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Leaves");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Departments");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Attendences");
        }
    }
}
