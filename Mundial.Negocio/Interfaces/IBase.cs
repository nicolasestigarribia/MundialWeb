using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Mundial.Negocio
{
    public interface IBase<T> where T : class
    {
        IEnumerable<T> GetAllByCondition(Expression<Func<T, bool>> where);

        T GetByCondition(Expression<Func<T, bool>> where);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        T GetById(int id);

        bool Insert(T model);

        bool Update(T model);

        bool Delete(T model);

        bool Save();
    }
}
