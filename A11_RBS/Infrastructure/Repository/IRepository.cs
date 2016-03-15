using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace A11_RBS.Infrastructure.Repository
{
    public interface IRepository<T>
    {


        T GetById(string Id);
        T Get(T entity);
        List<T> Get(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
       IList<T> GetAll();
        IQueryable<T> GetQueryable();
        T Add(T entity);
        bool Edit(Guid Id);
        bool Edit(T entity);
        bool Delete(Guid id);
        bool Delete(T entity);
       
    }
}