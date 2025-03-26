using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CHLeMudra.Data
{
    
    public  class GenericRepository<C, T> :
      IGenericRepository<T> where T : class where C : DbContext, IDisposable, new()
    {

        private C _entities;
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
        public GenericRepository()
        {
            _entities = new C();
        }
        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            try
            {
                _entities.Set<T>().Add(entity);
            }
            catch (Exception ex)
            {
                Logger.Error("GenericRepository.Add(T entity) : " + ex.Message, ex);
            }
        }

        public virtual bool Delete(T entity)
        {
            try
            {
                _entities.Set<T>().Remove(entity);
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("GenericRepository.Delete(T entity) : " + ex.Message, ex);
            }
            return false;
        }

        public virtual void Edit(T entity)
        {
            try
            {
                _entities.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                Logger.Error("GenericRepository.Edit(T entity) : " + ex.Message, ex);
            }
        }

        public virtual void Save()
        {
            try
            {
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("GenericRepository.Save() : " + ex.Message, ex);
            }
        }
        public virtual bool SaveList(List<T> entity)
        {
            try
            {
                _entities.Set<T>().AddRange(entity);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("GenericRepository.SaveList(List<T> entity) : " + ex.Message, ex);
            }
            return false;
        }
        public virtual bool Save(T entity)
        {
            try
            {
                _entities.Set<T>().Add(entity);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("GenericRepository.Save(T entity) : " + ex.Message, ex);
            }
            return false;
        }
        public virtual T SaveReturnModel(T entity)
        {
            try
            {
                _entities.Set<T>().Add(entity);
                _entities.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error("GenericRepository.SaveReturnModel(T entity) : " + ex.Message, ex);
            }
            return null;
        }
        public virtual bool Update(T entity)
        {
            try
            {
                _entities.Entry(entity).State = EntityState.Modified;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("GenericRepository.Update(T entity) : " + ex.Message, ex);

                throw ex;

            }
#pragma warning disable CS0162 // Unreachable code detected
            return false;
#pragma warning restore CS0162 // Unreachable code detected
        }

        public virtual T UpdateReturnModel(T entity)
        {
            try
            {
                _entities.Entry(entity).State = EntityState.Modified;
                _entities.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error("GenericRepository.Update(T entity) : " + ex.Message, ex);
            }
            return null;
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
