using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Task.Api.Data;
using Task.Api.DTOS;
using Task.Api.models;

namespace Task.Api.Controllers

{
  
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase

    {
        private readonly IAuthRepository _repo;
         private readonly IConfiguration _config;
         private readonly DataContext _context;
        private readonly ITokenInterface _token;

        public AuthController(IAuthRepository repo, IConfiguration config, DataContext context, ITokenInterface Token)
        {
            _repo = repo;
            _config = config;
            _context = context;
            _token = Token;
        }

        // [HttpPost("register")]
        // public async Task<ActionResult<UserDto>> Register (UserforRegister userforRegisterDto)
        // {

        //     userforRegisterDto.username = userforRegisterDto.username.ToLower();
        //     using var hmac = new System.Security.Cryptography.HMACSHA512();

        //     if(await _repo.UserExists(userforRegisterDto.username))
        //        return BadRequest("Username already exists");
            
        //     var user = new User{

        //         Name = userforRegisterDto.username.ToLower(),
        //         PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userforRegisterDto.Password)),
        //         PasswordSalt = hmac.Key


        //     }
        //     _

        //     return new UserDto
        //     {
        //         username = 
        //     }

        // }

        [AllowAnonymous]
        [HttpPost("register")]

        public async Task<IActionResult> Register(UserforRegister userforRegisterDto )
        {


            userforRegisterDto.username = userforRegisterDto.username.ToLower();
            if(await _repo.UserExists(userforRegisterDto.username))
               return BadRequest("Username already exists");
            
            var userToCreate = new User
            {
                Name = userforRegisterDto.username
            };

            var createdUser = await _repo.Register(userToCreate,userforRegisterDto.Password);

            return StatusCode(201);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(Userforlogin userforlogin )
        {

            var userfromrepo = await _repo.Login(userforlogin.username.ToLower(), userforlogin.Password);

            if (userfromrepo==null)
               return BadRequest("Username already exists");
               
            // TOKEN BUILDING

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userfromrepo.ID.ToString()),
                new Claim(ClaimTypes.Name, userfromrepo.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();

            var token = tokenhandler.CreateToken(tokenDescriptor);

            return Ok(new {

                User = userforlogin.username,
                token = tokenhandler.WriteToken(token)
            });

        }
        [AllowAnonymous]
        [HttpGet] 
        public async Task<IActionResult> GetUsers(){


            return Ok(await _repo.Getuserasync());

        }
        [Authorize]

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetUsers(int id){
              
               return Ok(await _repo.Getusersasync(id));
        }
    
    }


}