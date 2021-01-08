
using Task.Api.models;
   
namespace Task.Api.Data
   
{


        public interface ITokenInterface
        {

               string CreateToken(User user);

        }   

}