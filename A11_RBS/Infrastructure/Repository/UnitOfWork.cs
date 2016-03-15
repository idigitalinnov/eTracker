using A11_RBS.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A11_RBS.Infrastructure.Repository
{
    public class UnitOfWork 
    {

        public ApplicationDbContext Context;
      private Hashtable _repositories;
        /*
        private Repository<ApplicationUser> userRepository; 
        private Repository<Leaves> leaveRepository;           
         private Repository<SalaryDetails> salaryRepository ;
         private Repository<Attendence> attendenceRepository;
         private Repository<Timesheet> timesheetRepository;
         private Repository<Departments> departmentsRepository;
        */

         public UnitOfWork()
         {
           Context = new ApplicationDbContext();
         }
        /*
         public ApplicationDbContext Context
         {
             get { return context; }
         }

        */
        /*
        public Repository<ApplicationUser> UserRepository 
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new Repository<ApplicationUser>(context);
                }
                return userRepository;
            } 
        }
        public Repository<Leaves> LeaveRepository
        {
            get
            {
                if (this.leaveRepository == null)
                {
                    this.leaveRepository = new Repository<Leaves>(context);
                }
                return leaveRepository;
            }
        }
        public Repository<SalaryDetails> SalaryRepository
        {
            get
            {
                if (this.salaryRepository == null)
                {
                    this.salaryRepository = new Repository<SalaryDetails>(context);
                }
                return salaryRepository;
            }
        }
        public Repository<Attendence> AttendenceRepository
        {
            get
            {
                if (this.attendenceRepository == null)
                {
                    this.attendenceRepository = new Repository<Attendence>(context);
                }
                return attendenceRepository;
            }
        }
        public Repository<Timesheet> TimeSheetRepository
        {
            get
            {
                if (this.timesheetRepository == null)
                {
                    this.timesheetRepository = new Repository<Timesheet>(context);
                }
                return timesheetRepository;
            }
        }
        public Repository<Departments> DepartmentRepository
        {
            get
            {
                if (this.departmentsRepository == null)
                {
                    this.departmentsRepository = new Repository<Departments>(context);
                }
                return departmentsRepository;
            }
        }
        */


        /*
         public void Save()
         {
             Context.SaveChanges();
         }*/

         public IRepository<T> Repository<T>() where T : class
         {
             if (_repositories == null)
                 _repositories = new Hashtable();

             var type = typeof(T).Name;

             if (!_repositories.ContainsKey(type))
             {
                 var repositoryType = typeof(IRepository<>);

                 var repositoryInstance =
                     Activator.CreateInstance(repositoryType
                                                  .MakeGenericType(typeof(T)), Context);

                 _repositories.Add(type, repositoryInstance);
             }

             return (IRepository<T>)_repositories[type];
         }
        /*
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       */

    }
}