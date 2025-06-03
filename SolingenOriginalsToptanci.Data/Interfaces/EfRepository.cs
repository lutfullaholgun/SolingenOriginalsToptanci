using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Data.Interfaces;
using SolingenOriginalsToptanci.Data;

namespace SolingenOriginalsToptanci.Data.Repositories
{
    /// <summary>
    /// IRepository<T> arayüzünün EF Core tabanlı implementasyonu.
    /// </summary>
    /// <typeparam name="T">Entity tipi (Product, CustomerInfo vb.)</typeparam>
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly SolingenContext _context;
        private readonly DbSet<T> _dbSet;

        public EfRepository(SolingenContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync(); // KAYDETMEYİ UNUTMA
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
