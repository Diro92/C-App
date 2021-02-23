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

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers(Userforlogin userforlogin)
        {
            var users = await _userRepository.GetMembersAsync();

            // var users = await _userRepository.Getuserasync();

            // var Userstoreturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            return Ok(users);

        }
       
        
        [HttpGet("{name}")] 
        public async Task<ActionResult> GetUser(string name){
              
            var specificUser =  await _userRepository.GetMemberAsync(name);

            //  var users = await _userRepository.Getuserasync();

            // var specificUser = _mapper.Map<IEnumerable<MemberDto>>(users).Where(x => x.Name == name);

            return Ok(specificUser);

        }

        [HttpDelete("delete-Task/{Taskid}")]

        public async Task<ActionResult> DeleteTask(int taskid)
        {
                // var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = await _userRepository.GetUserByUsernameAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var tarea =user.Tareas.FirstOrDefault(x =>x.ID == taskid);

                // Console.WriteLine(tarea);

                if(tarea==null) return NotFound();

                user.Tareas.Remove(tarea);

                return Ok();
        }
        
    
    }

}