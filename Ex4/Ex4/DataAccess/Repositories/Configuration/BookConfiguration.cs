using Microsoft.EntityFrameworkCore;
using Ex4.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ex4.DataAccess.Repositories.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
            builder.HasOne(b => b.Author)
                   .WithMany(a => a.Books)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Book { Id = 1, Title = "1984", PublishedYear = 1949, AuthorId = 1 },
                new Book { Id = 2, Title = "Скотный двор", PublishedYear = 1945, AuthorId = 1 },
                new Book { Id = 3, Title = "Дочь священника", PublishedYear = 1935, AuthorId = 1 },
                new Book { Id = 4, Title = "451 градус по Фаренгейту", PublishedYear = 1953, AuthorId = 2 },
                new Book { Id = 5, Title = "Марсианские хроники", PublishedYear = 1950, AuthorId = 2 },
                new Book { Id = 6, Title = "Вино из одуванчиков", PublishedYear = 1957, AuthorId = 2 }
            );
        }
    }
}
