using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Task.Api.Data;
using Task.Api.DTOS;
using Task.Api.models;
using Microsoft.EntityFrameworkCore;

namespace Task.Api.Controllers

{

     [ApiController]
     [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly DataContext _context;
        private readonly ITokenInterface _tokenservice;

        public AuthenticationController(IAuthRepository repo, DataContext context, ITokenInterface tokenservice)
        {
            _repo = repo;
            _context = context;
            _tokenservice = tokenservice;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register (UserforRegister userforRegister)
        {
            if(await UserExist(userforRegister.username)) return BadRequest("Username already taken");
            using var hmac = new HMACSHA512();

            var user = new User
            {
                Name = userforRegister.username.ToLower(),
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userforRegister.Password))
            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                username = user.Name,
                Token = _tokenservice.CreateToken(user)

            };
        }

         [HttpPost("login")]

         public async Task<ActionResult<UserDto>> Login (Userforlogin userforlogin)
         
         {
             var user = await _context.Users.SingleOrDefaultAsync(x => x.Name == userforlogin.username);

                if(user==null) return Unauthorized("Invalid username");
             
             using var hmac = new HMACSHA512(user.PasswordSalt);

             var computerhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userforlogin.Password));

              for (int i=0; i<computerhash.Length; i++){

                   if(computerhash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid password");
               }

                 return new UserDto

               {
                username = user.Name,
                Token = _tokenservice.CreateToken(user)

                };

         }


        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.Name== username.ToLower());
        }
    }
}