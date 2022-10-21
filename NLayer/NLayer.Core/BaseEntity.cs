using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    /// <summary>
    /// BaseEntity sınıfımızın nesne öreneğini(instance) oluşturmayacağımız için abstract olarak tanımladık.
    ///
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
