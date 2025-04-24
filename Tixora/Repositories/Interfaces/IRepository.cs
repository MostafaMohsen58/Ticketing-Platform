namespace Tixora.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
<<<<<<< Updated upstream
        public void Add(T obj);
        public void Update(T obj);

        public int Save();
=======
        public Task AddAsync(T obj);
        public Task UpdateAsync(T obj);
        public Task<int> SaveAsync();
>>>>>>> Stashed changes
    }
}
