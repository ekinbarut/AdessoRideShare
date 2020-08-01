using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Service
{
    public abstract class EFDataChannel<T, context> : IDisposable
        where T : class
        where context : DbContext, new()
    {
        #region [ Private Member(s) ]

        protected context Context { get; set; }

        protected Type CurrentType { get; set; }

        #endregion

        #region [ Constructor(s) ]

        public EFDataChannel()
        {
            Context = new context();
            this.CurrentType = typeof(T);
        }

        #endregion

        #region [ CRUD Actions ]

        public virtual int Insert(T entity)
        {
            try
            {
                this.Context.Set<T>().Add(entity);

                return Commit();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on insert {0}.", typeof(T).Name), exc);
            }
        }

        public virtual bool Delete(T entity, bool permanently = false)
        {
            try
            {

                this.Context.Set<T>().Attach(entity);
                if (permanently)
                {
                    this.Context.Entry<T>(entity).State = EntityState.Deleted;
                }
                this.Context.Set<T>().Remove(entity);
                this.Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on delete {0}.", typeof(T).Name), exc);

            }
        }

        public virtual void Delete(Expression<Func<T, bool>> filterExpression, bool permanently = false)
        {
            this.Context.Set<T>().RemoveRange(this.Context.Set<T>().Where(filterExpression));
        }

        public virtual int Update(T entity)
        {
            try
            {
                this.Context.Set<T>().Attach(entity);
                this.Context.Entry<T>(entity).State = EntityState.Modified;
                return Commit();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on update {0}.", typeof(T).Name), exc);
            }
        }

        #endregion

        #region [ Fetchs ]

        #region [ Get All ]

        public virtual IQueryable<T> GetAll()
        {
            return this.Context.Set<T>();
        }

        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.Context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual IQueryable<T> GetAll(params object[] includes)
        {
            try
            {
                IQueryable<T> query = this.Context.Set<T>();

                if (includes.Length > 0)
                {
                    query = query.Include(includes[0].ToString());
                    for (int i = 1; i < includes.Length; i++)
                    {
                        query = query.Include(includes[i].ToString());
                    }
                }

                return query;
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on find {0}.", typeof(T).Name), exc);
            }
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = this.Context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.Where(predicate);

            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on find {0}.", typeof(T).Name), exc);
            }
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params object[] includes)
        {
            try
            {
                IQueryable<T> query = this.Context.Set<T>();

                if (includes.Length > 0)
                {
                    query = query.Include(includes[0].ToString());
                    for (int i = 1; i < includes.Length; i++)
                    {
                        query = query.Include(includes[i].ToString());
                    }
                }

                return query.Where(predicate);
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on find {0}.", typeof(T).Name), exc);
            }
        }

        #endregion

        #region [ Find ]

        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return this.Context.Set<T>().FirstOrDefault(predicate);
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on find {0}.", typeof(T).Name), exc);
            }
        }

        public virtual T Find(params object[] keyValues)
        {
            return this.Context.Set<T>().Find(keyValues);
        }

        public virtual T Find<TKey>(Expression<Func<T, TKey>> sortExpression, bool isDesc, Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (isDesc)
                {
                    return this.Context.Set<T>().OrderBy(sortExpression).FirstOrDefault(predicate);

                }
                else
                {
                    return this.Context.Set<T>().OrderByDescending(sortExpression).FirstOrDefault(predicate);
                }
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on find {0}.", typeof(T).Name), exc);
            }
        }

        public virtual T Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = this.Context.Set<T>();
                if (includes.Length > 0)
                {
                    query = query.Include(includes[0]);
                    for (int i = 1; i < includes.Length; i++)
                    {
                        query = query.Include(includes[i]);
                    }
                }
                return query.FirstOrDefault<T>(predicate);
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on find {0}.", typeof(T).Name), exc);
            }
        }

        public virtual T Find(Expression<Func<T, bool>> predicate, params object[] includes)
        {
            try
            {
                IQueryable<T> query = this.Context.Set<T>();
                if (includes.Length > 0)
                {
                    query = query.Include(includes[0].ToString());
                    for (int i = 1; i < includes.Length; i++)
                    {
                        query = query.Include(includes[i].ToString());
                    }
                }
                return query.FirstOrDefault<T>(predicate);
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on find {0}.", typeof(T).Name), exc);
            }
        }

        #endregion

        #region [ Get List ]

        public virtual List<T> GetList(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return this.Context.Set<T>().Where(predicate).ToList<T>();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get list of {0}.", typeof(T).Name), exc);
            }
        }

        public async virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await this.Context.Set<T>().Where(predicate).ToListAsync<T>();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get list of {0}.", typeof(T).Name), exc);
            }
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> predicate, params object[] includes)
        {
            try
            {
                IQueryable<T> query = this.Context.Set<T>();
                if (includes.Length > 0)
                {
                    query = query.Include(includes[0].ToString());
                    for (int i = 1; i < includes.Length; i++)
                    {
                        query = query.Include(includes[i].ToString());
                    }
                }
                return query.Where(predicate).ToList<T>();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get list of {0}.", typeof(T).Name), exc);
            }
        }

        public async virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, params object[] includes)
        {
            try
            {
                IQueryable<T> query = this.Context.Set<T>();
                if (includes.Length > 0)
                {
                    query = query.Include(includes[0].ToString());
                    for (int i = 1; i < includes.Length; i++)
                    {
                        query = query.Include(includes[i].ToString());
                    }
                }
                return await query.Where(predicate).ToListAsync<T>();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get list of {0}.", typeof(T).Name), exc);
            }
        }

        public virtual List<T> GetList<TKey>(Expression<Func<T, TKey>> sortExpression)
        {
            try
            {
                return this.Context.Set<T>().OrderByDescending(sortExpression).ToList<T>();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get list of {0}.", typeof(T).Name), exc);
            }
        }

        public virtual List<T> GetList<TKey>(Expression<Func<T, TKey>> sortExpression, params object[] includes)
        {
            try
            {
                IQueryable<T> query = this.Context.Set<T>();
                if (includes.Length > 0)
                {
                    query = query.Include(includes[0].ToString());
                    for (int i = 1; i < includes.Length; i++)
                    {
                        query = query.Include(includes[i].ToString());
                    }
                }
                return query.OrderByDescending(sortExpression).ToList<T>();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get list of {0}.", typeof(T).Name), exc);
            }
        }

        public virtual List<T> GetList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> sortExpression, params object[] includes)
        {
            try
            {
                IQueryable<T> query = this.Context.Set<T>();
                if (includes.Length > 0)
                {
                    query = query.Include(includes[0].ToString());
                    for (int i = 1; i < includes.Length; i++)
                    {
                        query = query.Include(includes[i].ToString());
                    }
                }
                return query.Where(predicate).OrderByDescending(sortExpression).ToList<T>();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get list of {0}.", typeof(T).Name), exc);
            }
        }

        public virtual List<T> GetList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> sortExpression, int pageSize, int pageIndex)
        {
            try
            {
                return this.Context.Set<T>().Where(predicate).OrderByDescending(sortExpression).Skip(pageSize * pageIndex).Take(pageSize).ToList<T>();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get list of {0}.", typeof(T).Name), exc);
            }
        }

        public virtual List<T> GetList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> sortExpression, bool isDesc, int pageSize, int pageIndex, params object[] includes)
        {
            try
            {
                IQueryable<T> query = this.Context.Set<T>();
                if (includes.Length > 0)
                {
                    query = query.Include(includes[0].ToString());
                    for (int i = 1; i < includes.Length; i++)
                    {
                        query = query.Include(includes[i].ToString());
                    }
                }
                if (isDesc)
                {
                    return query.Where(predicate).OrderByDescending(sortExpression).Skip(pageSize * pageIndex).Take(pageSize).ToList<T>();
                }
                else
                {
                    return query.Where(predicate).OrderBy(sortExpression).Skip(pageSize * pageIndex).Take(pageSize).ToList<T>();
                }
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get list of {0}.", typeof(T).Name), exc);
            }
        }

        public virtual List<T> GetList<TKey>(Expression<Func<T, TKey>> sortExpression, bool isDesc, int pageSize, int pageIndex)
        {
            try
            {
                if (isDesc)
                {
                    return this.Context.Set<T>().OrderByDescending(sortExpression).Skip(pageSize * pageIndex).Take(pageSize).ToList<T>();
                }
                else
                {
                    return this.Context.Set<T>().OrderBy(sortExpression).Skip(pageSize * pageIndex).Take(pageSize).ToList<T>();
                }

            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get list of {0}.", typeof(T).Name), exc);
            }
        }

        #endregion

        #endregion

        #region [ Scalars ]

        public virtual int CountOfRecord()
        {
            try
            {
                return this.Context.Set<T>().Count();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get count of {0}.", typeof(T).Name), exc);
            }
        }

        public virtual bool Exist(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return this.Context.Set<T>().Any(predicate);
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on checking if data exists: {0}.", typeof(T).Name), exc);
            }
        }

        public virtual int CountOfRecord(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return this.Context.Set<T>().Count(predicate);
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Error on get count of {0}.", typeof(T).Name), exc);
            }
        }

        public virtual TResult GetMax<TResult>(Expression<Func<T, TResult>> selector)
        {
            return this.Context.Set<T>().Max(selector);
        }

        #endregion

        #region [ Helper functions ]

        private int Commit()
        {
            var validationErrors = this.Context.ChangeTracker
                    .Entries<IValidatableObject>()
                    .SelectMany(e => e.Entity.Validate(null))
                    .Where(r => r != ValidationResult.Success);

            if (validationErrors.Any())
            {
                string finalMessage = string.Empty;
                foreach (var validationError in validationErrors)
                {
                    string message = validationError.ErrorMessage;
                    finalMessage += message + Environment.NewLine;
                }

                throw new Exception(finalMessage);
            }

            return this.Context.SaveChanges();
        }

        #endregion

        #region [ Dispose ]

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }

        #endregion
    }
}
