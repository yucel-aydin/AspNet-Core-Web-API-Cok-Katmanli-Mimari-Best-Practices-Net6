using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds
{
    internal class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category
            {
                Id = 1,
                Name = "Kalemler",
                CreatedDate = DateTime.Now
            },
            new Category
            {
                Id = 2,
                Name = "Kitaplar",
                CreatedDate = DateTime.Now
            },
            new Category
            {
                Id = 3,
                Name = "Defterler",
                CreatedDate = DateTime.Now
            });
        }
    }
}
