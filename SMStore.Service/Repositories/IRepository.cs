using System;
using System.Collections.Generic;
using System.Linq.Expressions; // Expression lar veriyi sorgularken lambda expression kullanbilmemiz sağlar. Örneğin p=>p.Id = id gibi
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SMStore.Service.Repositories
{
    public interface IRepository<T> where T : class // IRepository interface i <T> yapısı ile generic hale getirildi. Buradaki T dışarıdan gönderilecek veri(class, değişen vb). where T : class ise buraya gönderilecek parametre class olmak zorundadır demek. ortak kullanım için bir tek klasöre özel değil her yerde kullanilabilir.
    {
        // Buarada Uygulamada kullanacağımız crud metotlarının imzalarını belirliyoruz. 
        List<T> GetAll(); // verilerin hepsini getirecek metot
        List<T> GetAll(Expression<Func<T, bool>> expression); // uygulamada bu metodu lambda expression ile kullanbilmek için
        T Get(Expression<Func<T, bool>> expression);
        T Find(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int SaveChanges();

        // Asenktron Metotlar
        Task<T> FindAsync(int id);
        Task<T> FirstorDefaultAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity); // Asenkron void metot
        Task<int> SaveChangesAsync();

    }
}
