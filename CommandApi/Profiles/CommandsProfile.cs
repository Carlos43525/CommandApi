using AutoMapper;
using CommandApi.App.Dtos;
using CommandApi.Models;

namespace CommandApi.App.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>(); 
            CreateMap<CommandUpdateDto, Command>();            
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}
