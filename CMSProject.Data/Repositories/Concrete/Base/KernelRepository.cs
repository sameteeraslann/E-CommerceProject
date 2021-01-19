using CMSProject.Data.Context;
using CMSProject.Data.Repositories.Interfaces.Base;
using CMSProject.Entity.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMSProject.Data.Repositories.Concrete.Base
{

    // KernelRepository=> IKernelRepository'den implement yönetimi ile aldığımız methodlarımıza gövde kazandırıyoruz. 
    public class KernelRepository<T> : IKernelRepository<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext _context; 
        protected DbSet<T> _table;
        

        public KernelRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _table = _context.Set<T>(); // Bu sayede her satırda _context.Set<T>() yazmaktan kaçtık.
                   
        }
        public async Task Add(T entity)
        {
             await _table.AddAsync(entity);
             await _context.SaveChangesAsync();
        }

        // Asenkron programlama yaptığımızdan burada ki işlemlerimize "async" işaretlenir ve methodların gövdelerini de "await" olarak işaretlemek zorundayız.

        public async Task<bool> Any(Expression<Func<T, bool>> expression = null)
        {
            return await _table.AnyAsync(expression);
        }

        public async Task Delete(T entity)
        {
             _table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> expression) => await _table.Where(expression).FirstOrDefaultAsync();

        public async Task<List<T>> Get(Expression<Func<T, bool>> expression)
        {
            return await _table.Where(expression).ToListAsync();
        }

        public async Task<List<T>> GetAll() => await _table.ToListAsync();

        public async Task<T> GetById(int id) => await _table.FindAsync(id);

        public async Task Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
