using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Create(TEntity model);

        List<TEntity> Create(List<TEntity> model);

        bool Update(TEntity model);

        bool Update(List<TEntity> model);

        bool Delete(TEntity model);

        bool Delete(params object[] Keys);

        bool Delete(Expression<Func<TEntity, bool>> where);

        int Save();

        TEntity Find(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);
    }
}
