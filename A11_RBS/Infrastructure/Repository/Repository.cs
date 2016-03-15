using A11_RBS.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace A11_RBS.Infrastructure.Repository
{
    public class Repository<T>:IRepository<T> where T:class
    {
  //ApplicationDbContext context;
   //  internal DbSet<T> dbset;
      UnitOfWork _unitOfWork;

         public Repository()
            : this(new UnitOfWork())
        {
        }

       public Repository(UnitOfWork unitOfWork)
        {   
           
          _unitOfWork = unitOfWork ?? new UnitOfWork();
        }


        public T GetById(string Id)
        {

            return _unitOfWork.Context.Set<T>().Find(Id);
        }

        public IList<T> GetAll()
        {
            try
            {
                return _unitOfWork.Context.Set<T>().ToList();
            }
            catch
            {
                return null;

            }
           
        }


        public T Get(T entity)
        {
            try
            {
                return _unitOfWork.Context.Set<T>().Find(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
        public List<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _unitOfWork.Context.Set<T>().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public IQueryable<T> GetQueryable()
        {
            

            try
            {
                return _unitOfWork.Context.Set<T>().AsQueryable<T>();

            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.StackTrace);
                return null;
            }


        }
        public T Add(T entity)
        {
         // try
         // {
            var v = _unitOfWork.Context.Set<T>().Add(entity);

            _unitOfWork.Context.SaveChanges();
return v;

         /* }
            catch
            {
                return null;
            }*/
        }
        public bool Edit(Guid Id)
        {
            try
            {
          T entity = _unitOfWork.Context.Set<T>().Find(Id);
           _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
           _unitOfWork.Context.SaveChanges();
           return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public bool Edit(T entity)
        {
            try
            {
                 _unitOfWork.Context.Set<T>().Attach(entity);
                _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
                _unitOfWork.Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }


        public  bool Delete(Guid id)
        {
            try
            {
                var entityToRemove = GetById(id.ToString());
                _unitOfWork.Context.Set<T>().Remove(entityToRemove);
                _unitOfWork.Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            };
        }

        public bool Delete(T entity)
        {
            try
            {
                var entityToRemove = Get(entity);
                _unitOfWork.Context.Set<T>().Remove(entityToRemove);
                _unitOfWork.Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }

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
         * */
    }
}