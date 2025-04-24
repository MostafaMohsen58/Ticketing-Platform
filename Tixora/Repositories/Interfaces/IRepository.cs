namespace Tixora.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T obj);
        void Update(T obj);
        int Save();

    }
}
