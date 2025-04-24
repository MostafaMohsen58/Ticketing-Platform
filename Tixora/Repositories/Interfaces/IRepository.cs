namespace Tixora.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public void Add(T obj);
        public void Update(T obj);
        public int Save();
    }
}
