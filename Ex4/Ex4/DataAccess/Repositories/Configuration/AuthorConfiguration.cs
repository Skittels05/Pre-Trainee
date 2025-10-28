using Microsoft.EntityFrameworkCore;
using Ex4.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ex4.DataAccess.Repositories.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);

            builder.HasData(
                new Author
                {
                    Id = 1,
                    Name = "Джордж Оруэлл",
                    DateOfBirth = new DateTime(1903, 6, 25)
                },
                new Author
                {
                    Id = 2,
                    Name = "Рэй Брэдбери",
                    DateOfBirth = new DateTime(1920, 8, 22)
                }
                );
        }
    }
}
