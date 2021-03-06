using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Api.DTOS;
using Task.Api.models;

namespace Task.Api.Interfaces
{
    public interface IUserRepository
    {

        void Update(User User);

        Task<bool> SaveAllSync();


        Task<IEnumerable<User>> Getusersync();

        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);

       



    }
}