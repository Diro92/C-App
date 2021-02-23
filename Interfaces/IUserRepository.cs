using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Api.DTOS;
using Task.Api.models;

namespace Task.Api.Interfaces
{
    public interface IUserRepository
    {

        void Update(User User);

        Task<IEnumerable<User>> Getuserasync();

        Task<User> Getusersasync(int id);

        Task<IEnumerable<MemberDto>> GetMembersAsync();

        Task<MemberDto> GetMemberAsync(string username);

        Task<MemberDto> GetUserByUsernameAsync(string username);
    }
}