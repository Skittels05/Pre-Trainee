using Ex4.BusinessLogic.Interfaces;
using Ex4.BusinessLogic.Models;
using Ex4.BusinessLogic.Services;
using Ex4.BusinessLogic.Validators;
using Ex4.DataAccess;
using Ex4.DataAccess.Interfaces;
using Ex4.DataAccess.Repositories;
using Ex4.UserInterface;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ex4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("SQLServerConnectionString");
            builder.Services.AddDbContextPool<LibraryContext>(
                options=>options.UseSqlServer(connectionString));

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidationExceptionFilter>();
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();

            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookService, BookService>();

            builder.Services.AddScoped<IValidator<Author>, AuthorValidator>();
            builder.Services.AddScoped<IValidator<Book>, BookValidator>();

            builder.Services.AddProblemDetails();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler();

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
