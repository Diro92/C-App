using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task.Api.Data;
using Task.Api.models;

namespace Task.Api.Interfaces
   
{
    public class UserRepository : IUserRepository

    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> Getuserasync()
        {


           return await _context.Users.Include(t =>t.Tareas).ToListAsync();
        }

        public async Task<User> Getusersasync(int id)
        {
             return await _context.Users.FindAsync(id);
        }

        public void Update(User User)
        {
            throw new System.NotImplementedException();
        }
    }



}
