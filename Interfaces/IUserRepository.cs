using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Api.models;
   
namespace Task.Api.Interfaces
   
{
    public interface IUserRepository
    
    {

        void Update(User User);

        Task<IEnumerable<User>> Getuserasync();

        Task<User> Getusersasync(int id);

     

        


    }



}