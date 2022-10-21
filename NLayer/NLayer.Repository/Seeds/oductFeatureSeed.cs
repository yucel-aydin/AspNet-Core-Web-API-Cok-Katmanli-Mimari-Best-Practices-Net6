using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;

namespace NLayer.Repository.Seeds
{
    internal class oductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(new ProductFeature
            {
                Id = 1,
                Color = "Kırmızı",
                Height = 20,
                Width = 10,
                ProductId = 1

            },
            new ProductFeature
            {
                Id = 2,
                Color = "Mavi",
                Height = 150,
                Width = 100,
                ProductId = 2
            });
        }
    }
}