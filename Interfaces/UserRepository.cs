using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Task.Api.Data;
using Task.Api.DTOS;
using Task.Api.models;

namespace Task.Api.Interfaces
   
{
    public class UserRepository :IUserRepository

    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> Getusersync()
        {
           return await _context.Users
                        .Include(p =>p.Tareas)
                        .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
             return await _context.Users.FindAsync(id);
           
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                         .Include(p => p.Tareas)
                         .SingleOrDefaultAsync(x => x.Name == username);
        }

        public async Task<bool> SaveAllSync()
        {
            return  await _context.SaveChangesAsync() > 0;
        }

        public void Update(User user)
        {
           _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
                   .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                   .ToListAsync();
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                   .Where(x=>x.Name ==username)
                   .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                   .SingleOrDefaultAsync();
        }
    }

    



}
