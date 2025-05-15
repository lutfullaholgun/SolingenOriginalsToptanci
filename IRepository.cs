using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolingenOriginalsToptanci.Data.Interfaces
{
    /// <summary>
    /// Genel CRUD operasyonlarını tanımlar.
    /// </summary>
    /// <typeparam name="T">Entity tipi (Product, CustomerInfo vb.)</typeparam>
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
