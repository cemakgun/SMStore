using Microsoft.EntityFrameworkCore;
using SMStore.Data;
using SMStore.Entities;
using System.Linq.Expressions;

namespace SMStore.Service.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()

        // T kısmına gönderilecek olan yapı için şartlar koyduk.T bir class olmak zorunda, IEntity interface inden gelmek zorunda ve new lenebilir bir tip olmak zorunda
    {
        internal readonly DatabaseContext _databaseContext;
        public Repository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Set<T>();
        }
        DbSet<T> _dbSet; // DatabaseContext teki DbSet leri Generic olarak kullanbilmemiz için

        public void Add(T entity) // eğer bu metodun dönüş tipini void yerine int yapsaydık
        {
            _dbSet.Add(entity);
            // Burada geriye return ile save change i dönmemiz gerekirdi
           
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity); // parametreyle gelecek class i asenkron olarak ekle
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Find(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> FirstorDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public int SaveChanges()
        {
            return _databaseContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _databaseContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _databaseContext.Update(entity);
        }

        // ornek Ibrandrepository de yaratabilir, özel görevler yüklemek için.
    }

}
