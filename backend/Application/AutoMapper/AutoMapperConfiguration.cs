using Application.DTO;
using Application.DTO.Post;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserListDTO, User>().ReverseMap();
            CreateMap<UserUpdateDTO, User>().ReverseMap();
            CreateMap<PostImageCreateDTO, PostImages>().ReverseMap();
            CreateMap<PostCreateDTO, Post>().ReverseMap();
            CreateMap<PostListAllDTO, Post>().ReverseMap()
                .ForMember(p => p.PhotoUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault().FileUrl))
                .ForMember(p => p.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.ToString("dd/MM/yy hh:mm")));
            CreateMap<PostPublishedDTO, Post>().ReverseMap();
        }
    }
}
