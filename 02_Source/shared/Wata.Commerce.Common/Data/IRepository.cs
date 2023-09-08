namespace Wata.Commerce.Common.Data
{
    public interface IRepository<T>
    {
        Task<T?> InsertAsync(T obj);
        Task<int> UpdateAsync(T obj);
        Task<int> DeleteAsync(object ids);
        Task<T?> GetAsync(object ids);
        Task<List<T>> GetAllAsync();
    }
}