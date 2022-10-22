using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    /// <summary>
    /// IGenericRepository ile aynı gibi dönüş tipleri farklılaşacağı için bu şekilde tanımlar.

    /// </summary>
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        // List yerine IQueryable kullandığımızda yazılan sorgular direk db ye gitmez .toList(), .toListAsync() yaptıktan sonra db ye gider.
        // Yani sorgu yapmıyor sorguyu oluşturuyoruz sadece.
        //Func<T, bool> default delegate tir.
        //"Where(x=>x.id==5)" burada x T ye karşılık gelir. x.id==5 sonucuda boola karşılık gelir.
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        // Burada service tarafında delete ve update ile birlikte savechange çalışacağı için async yaptık.
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
