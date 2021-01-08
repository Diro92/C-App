
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task.Api.models;
   
   
   
   namespace Task.Api.Data
   
{
    public class AuthRepository :IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        
        public async Task<User> Login(string username, string password)
        {
             var user  = await _context.Users.FirstOrDefaultAsync(x=> x.Name == username);
          
             if(user==null)
                return null;

            if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
                return null;

            return user;

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               
               var computedhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               for (int i=0; i<computedhash.Length; i++){

                   if(computedhash[i]!=passwordHash[i]) return false;
               }

            }
            return true;

        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            
            CreatePassword(password, out passwordHash,out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

        }

        private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
               passwordSalt = hmac.Key;
               passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.Name ==username))
               
               return true;
            
            return false;
        }

        public async Task<IEnumerable<User>> Getuserasync()
        {
           return await _context.Users.ToListAsync();
        }

        public async Task<User> Getusersasync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }


}