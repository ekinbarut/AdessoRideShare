using ARS.Common.Models;
using ARS.Models.Models;
using ARS.Models.Responses;
using ARS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Service.Base
{
    public class EFServiceBase<T, DAO, CTX> : IServiceBase<ARSServiceResponse<T>, T>
       where T : Entity
       where DAO : EFDataChannel<T, CTX>, new()
       where CTX : DbContext, new()
    {
        #region [ Implementation of IService ]

        public ARSServiceResponse<T> Create(T model)
        {
            using (var bo = new DAO())
            {
                // Set the missing fields
                model.Created = DateTime.UtcNow;

                //(model as T).Customer.ContactInformation.ForEach(ci => ci.);
                int result = bo.Insert(model as T);
                if (result > 0)
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = new List<T>() { model }
                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                        Result = new List<T>() { model }
                    };
                }
            }
        }

        public ARSServiceResponse<T> Update(T model)
        {
            using (var bo = new DAO())
            {
                int result = bo.Update(model as T);
                if (result > 0)
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = new List<T>() { model }
                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                        Result = new List<T>() { model }
                    };
                }
            }
        }

        public ARSServiceResponse<T> Delete(T model)
        {
            using (var bo = new DAO())
            {
                bool isDeleted = bo.Delete(model as T, true);
                if (isDeleted)
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = new List<T>() { model }
                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                        Result = new List<T>() { model }
                    };
                }
            }
        }

        public ARSServiceResponse<T> Delete(Expression<Func<T, bool>> predicate)
        {
            using (var bo = new DAO())
            {
                var model = bo.Find(predicate);

                bool isDeleted = bo.Delete(model as T, true);
                if (isDeleted)
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = new List<T>() { model }
                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                        Result = new List<T>() { model }
                    };
                }
            }
        }

        public ARSServiceResponse<T> Find(Expression<Func<T, bool>> predicate)
        {
            using (var bo = new DAO())
            {
                T result = bo.Find(predicate) as T;

                if (result != null)
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = new List<T>() { result }
                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                    };
                }
            }
        }

        public ARSServiceResponse<T> FindIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            using (var bo = new DAO())
            {
                var result = bo.Find(predicate, includes);

                if (result != null)
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = new List<T>() { result }
                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                        Result = new List<T>() { }
                    };
                }
            }
        }

        public ARSServiceResponse<T> FindIncluding(Expression<Func<T, bool>> predicate, params object[] includes)
        {
            using (var bo = new DAO())
            {
                var result = bo.Find(predicate, includes);

                if (result != null)
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = new List<T>() { result }
                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                        Result = new List<T>() { }
                    };
                }
            }
        }

        public ARSServiceResponse<T> Get(Expression<Func<T, bool>> predicate)
        {
            using (var bo = new DAO())
            {
                var results = bo.GetList(predicate);

                if (results != null && results.Any())
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = results
                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                        Result = new List<T>()
                    };
                }
            }
        }

        public ARSServiceResponse<T> GetIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            using (var bo = new DAO())
            {
                var results = bo.GetList(predicate, includes);

                if (results != null && results.Any())
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = results
                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                        Result = new List<T>()
                    };
                }
            }
        }

        public ARSServiceResponse<T> GetIncluding(Expression<Func<T, bool>> predicate, params object[] includes)
        {
            using (var bo = new DAO())
            {
                var results = bo.GetList(predicate, includes);

                if (results != null && results.Any())
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = results
                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                        Result = new List<T>()
                    };
                }
            }
        }

        public ARSServiceResponse<T> GetAll()
        {
            using (var bo = new DAO())
            {
                List<T> result = bo.GetAll().ToList();
                if (result.Any())
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = result as List<T>,

                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                    };
                }
            }
        }

        public ARSServiceResponse<T> GetAllIncluding(params Expression<Func<T, object>>[] includes)
        {
            using (var bo = new DAO())
            {
                var result = bo.GetAllIncluding(includes).ToList();

                if (result.Any())
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = result as List<T>,

                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                        Result = new List<T>(),
                    };
                }
            }
        }

        public ARSServiceResponse<T> GetAllIncluding(params object[] includes)
        {
            using (var bo = new DAO())
            {
                var result = bo.GetAll(includes).ToList();

                if (result.Any())
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Success,
                        Result = result as List<T>,

                    };
                }
                else
                {
                    return new ARSServiceResponse<T>()
                    {
                        Type = ServiceResponseTypes.Error,
                        Result = new List<T>(),
                    };
                }
            }
        }

        #endregion

    }
}
