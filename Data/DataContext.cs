using Microsoft.EntityFrameworkCore;
using Task.Api.models;
namespace Task.Api.Data

{
    public class DataContext: DbContext
    
    {
        public DataContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<User>  Users {get;set;}


            
        
    }
}