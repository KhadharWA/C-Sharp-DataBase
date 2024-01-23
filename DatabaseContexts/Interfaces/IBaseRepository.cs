

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace Shared.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class 
{
    TEntity Create(TEntity entity);

    IEnumerable<TEntity> ReadAll();

    IEnumerable<TEntity> Read(int take);

    TEntity GetOne(Expression<Func<TEntity, bool>> predicate);

    TEntity Update(Expression<Func<TEntity, bool>> predicate, TEntity entity);

    bool Delete(Expression<Func<TEntity, bool>> predicate);

    bool Exists(Expression<Func<TEntity, bool>> predicate);
}
