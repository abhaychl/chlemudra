using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CHLeMudra.Api.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        bool Delete(T entity);
        void Edit(T entity);
        void Save();
        bool SaveList(List<T> entity);
        bool Save(T entity);
        bool Update(T entity);
        T SaveReturnModel(T entity);
        T UpdateReturnModel(T entity);
        void Dispose();
    }
}
