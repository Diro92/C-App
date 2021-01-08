
using AutoMapper;
using Task.Api.DTOS;
using Task.Api.models;

namespace Task.Api.Helpers

{

    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<User,MemberDto>();
            
            CreateMap<Tarea, TareaDTO>();
        }
    }

}