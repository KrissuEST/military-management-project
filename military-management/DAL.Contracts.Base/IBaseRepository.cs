using Domain.Contracts.Base;

namespace DAL.Contracts.Base;

// Default primary key comes here. Which is based on Guid.
public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid>
    where TEntity : class, IDomainEntityId
{
}

public interface IBaseRepository<TEntity, in TKey>
    where TEntity : class, IDomainEntityId<TKey> 
    where TKey : struct, IEquatable<TKey>
{  // Now we will support all possible primary key types.
    
    // IEnumerable<TEntity> All();
    Task<IEnumerable<TEntity>> AllAsync();

    // TEntity Find(TKey id);
    Task<TEntity?> FindAsync(TKey id);   // ? - optional

    TEntity Add(TEntity entity); 

    TEntity Update(TEntity entity);

    TEntity Remove(TEntity entity);

    Task<TEntity?> RemoveAsync(TKey id);
    // From outside, only these interface methods.
}