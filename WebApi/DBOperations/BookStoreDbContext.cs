using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace WebApi.DBOperations
{
public class BookStoreDbContext : DbContext
{
public BookStoreDbContext(DbContextOptions<BookStoreDbContext>options):base(options)
{  }
public DbSet<Book> Books {get; set;}
}

}