using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Api.models;
   
    namespace Task.Api.Data
   
{


        public interface IAuthRepository
        {

                Task<User> Register(User User, string password);
                Task<User> Login(string username, string password);

                Task<bool> UserExists(string username);

                 Task<IEnumerable<User>> Getuserasync();
                Task<User> Getusersasync(int id);

        }   

}