namespace NLayer.Core.Models
{
    /// <summary>
    /// BaseEntity sınıfımızın nesne öreneğini(instance) oluşturmayacağımız için abstract olarak tanımladık.
    ///
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
