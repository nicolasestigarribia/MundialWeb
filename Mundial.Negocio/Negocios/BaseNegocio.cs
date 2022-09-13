using Mundial.Negocio;
using Mundial.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace Mundial.Negocio
{ 
    public class BaseNegocio<T> : BLLContext, IBase<T> where T : class
    {
      

        public BaseNegocio()
        {
        }

        public bool Delete(T model)
        {
            Context.Set<T>().Remove(model);
            return Save();
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = Context.Set<T>().AsQueryable();

            query = PrepareQuery(query, predicate, include, orderBy);

            return query.ToList();
        }

        public IEnumerable<T> GetAllByCondition(Expression<Func<T, bool>> where)
        {
            return Context.Set<T>().Where(where).ToList();
        }

        public T GetByCondition(Expression<Func<T, bool>> where)
        {
            return Context.Set<T>().Where(where).SingleOrDefault();
        }

        public T GetById(int id)
        {
            var model = Context.Set<T>().Find(id);
            if (model == null)
                throw new Exception("Objeto no encontrado en la BD");
            return model;
        }

        public bool Insert(T model)
        {
            Context.Add(model);
            return Save();
        }

        public bool Save()
        {
            return Context.SaveChanges() > 0;
        }

        public bool Update(T model)
        {
            Context.Attach(model);
            Context.Entry(model).State = EntityState.Modified;
            return Save();
        }

        protected IQueryable<T> PrepareQuery(
            IQueryable<T> query,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
)
        {
            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }
    }
}
