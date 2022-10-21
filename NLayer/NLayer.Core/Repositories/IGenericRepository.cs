using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);


        // List yerine IQueryable kullandığımızda yazılan sorgular direk db ye gitmez .toList(), .toListAsync() yaptıktan sonra db ye gider.
        // Yani sorgu yapmıyor sorguyu oluşturuyoruz sadece.
        //Func<T, bool> default delegate tir.
        //"Where(x=>x.id==5)" burada x T ye karşılık gelir. x.id==5 sonucuda boola karşılık gelir.
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        //Update ve Delete(Remove) metotların Async versiyonları EF yoktur.
        //Çünkü zaten memory deki bir nesnenin sadece state'ini değiştiriyor.
        //Bu işlem zaten çok kısa süren bir işlem olduğu için eklenmemiştir.
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
