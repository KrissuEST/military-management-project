namespace Contracts.Base;

// We wanna map between output and input.
public interface IMapper<TSource, TDestination>
{
    // Maps from one entity to other entity.
    TDestination? Map(TSource? entity);
    TSource? Map(TDestination? entity);
}