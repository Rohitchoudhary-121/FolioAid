using Application.CommandResults.Models;
using Application.Commands.Account;
using Application.Commands.Project;
using AutoMapper;
using Domain.Entity;
using Domain.Entity.Identity;

namespace Application.Utilities
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, RegisterUserCommand>().ReverseMap();
            CreateMap<Project, AddProjectCommand>().ReverseMap();
            CreateMap<Project, ProjectDetailedResult>().ReverseMap();
            CreateMap<AddProjectResultModel, Project>().ReverseMap();
        }

    }
}
