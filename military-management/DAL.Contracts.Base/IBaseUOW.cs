namespace DAL.Contracts.Base;

// Unit of work
public interface IBaseUOW
{
    Task<int> SaveChangesAsync();
    // ?? how to contain and create repositories
}