using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Task.Api.Data;
using Task.Api.DTOS;
using Task.Api.Interfaces;
using Task.Api.models;
using System.Security.Claims;

namespace Task.Api.Controllers
{
     [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
       public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers(){

           var users = await _userRepository.GetMembersAsync();
           var userstoreturn = _mapper.Map<IEnumerable<MemberDto>>(users);
           
           return Ok(userstoreturn);
       }
        [Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username){

           return await _userRepository.GetMemberAsync(username);


        
        }
        
        // [HttpDelete("delete-task/{Taskid}")]
        // public async Task<ActionResult> DeleteTask (int taskid)
        // {
        //    var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;



        //    return BadRequest("va");

        // }

       
    }

}