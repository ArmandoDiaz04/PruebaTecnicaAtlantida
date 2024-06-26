using API.Core.Application.Contracts;
using API.Core.Domain.Generic;
using API.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Infraestructure.Services
{
    public class Repository<T> : IRepository<T> where T : BaseDomain
    {
        private readonly TarjetaCreditoDbContext db;

        public Repository(TarjetaCreditoDbContext db)
        {
            this.db = db;
        }

        public async Task DeleteAsync(int id)
        {
            db.Set<T>().Remove(await GetByIdAsync(id));
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await db.Set<T>().FirstOrDefaultAsync(p => p.id == id);
        }

        public IQueryable<T> GetQuery()
        {
            return db.Set<T>().AsQueryable();
        }

        public async Task InsertAsync(T entity)
        {
            await db.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
