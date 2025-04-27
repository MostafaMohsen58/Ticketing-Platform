namespace Tixora.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task AddAsync(T obj);
        public Task UpdateAsync(T obj);
        public Task<int> SaveAsync();
    }
}
