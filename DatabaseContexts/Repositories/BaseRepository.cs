using Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using Shared.Utils;


namespace Shared.Repositories;

public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : class where TContext : DbContext
{
    private readonly TContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private readonly IErrorLogger _logger;

    protected BaseRepository(TContext context, IErrorLogger errorLogger)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
        _logger = errorLogger;
    }

    // SQL: INSERT INTO Table VALUES (@Value_1, @Value_2 ...)
    // C# : Create(Entity);

    public virtual TEntity Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch (Exception ex) { _logger.Log (ex.Message, "Base.Create()", LogTypes.Error); }
        return null!;
    }

    // SQL: SELECT * FROM Table 
    // C# : ReadAll();

    public virtual IEnumerable<TEntity> ReadAll()
    {
        try
        {
           var entities = _dbSet.ToList();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { _logger.Log(ex.Message, "Base.ReadAll()", LogTypes.Error); }
        return null!;
    }

    // SQL: SELECT TOP(3) FROM Table 
    // C# : Read(3);

    public virtual IEnumerable<TEntity> Read(int take)
    {
        try
        {
            var entities = _dbSet.Take(take).ToList();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { _logger.Log(ex.Message, "Base.Read()", LogTypes.Error); }
        return null!;
    }

    // SQL: SELECT * FROM Table WHERE Id = @Id
    // C# : GetOne(x => x.Id == id);

    public virtual TEntity GetOne(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = _dbSet.FirstOrDefault(predicate);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { _logger.Log(ex.Message, "Base.GetOne()", LogTypes.Error); }
        return null!;
    }

    // SQL: UPDATE Table SET Column_1 = @Value_1, Column_2 = @Value_2 .... WHERE Id = @Id
    // C# : Update(x => x.Id == id, entity);

    public virtual TEntity Update(Expression<Func<TEntity, bool>> predicate, TEntity entity)
    {
        try
        {
            var existingEntity = _dbSet.FirstOrDefault(predicate);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.SaveChanges();
                return existingEntity;
            }

        }
        catch (Exception ex) { _logger.Log(ex.Message, "Base.Update()", LogTypes.Error); }
        return null!;
    }

    // SQL: DELETE FROM Table WHERE Id = @Id
    // C# : Delete(x => x.Id == id);

    public virtual bool Delete(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = _dbSet.FirstOrDefault(predicate);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
                return true;
            }
        }
        catch (Exception ex) { _logger.Log(ex.Message, "Base.Delete()", LogTypes.Error); }
        return false;
    }


    // SQL: SELECT 1 FROM Table WHERE Id = @Id
    // C# : Exists(x => x.Id == id);
    public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var found = _dbSet.Any(predicate);
            return found;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "Base.Exists()", LogTypes.Error); }
        return false;
    }
}


