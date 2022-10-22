namespace NLayer.Core.UnitOfWorks
{
    /// <summary>
    /// Veritabanına yapılacak işlemleri toplu bir şekilde tek bir transaction üzerinden yönetilmesine izin verir.
    /// Farklı repository de yapılan değişikler ayrı ayrı savechange çağrılarak db yazılır. 
    /// Unitofwork sayesinde farklı repodaki işlemler tek seferde db ye yazılır. Eğer birinde sorun olursa bütün işlemler geri alınır(Rolback) otomatik olarak.
    /// Savechanges kontrolünü sağlar.
    /// </summary>
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Comit();
    }
}
