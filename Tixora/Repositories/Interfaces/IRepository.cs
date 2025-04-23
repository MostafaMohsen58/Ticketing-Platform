namespace Tixora.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T obj);
        Task UpdateAsync(T obj);
        Task<int> SaveAsync();

    }
}
