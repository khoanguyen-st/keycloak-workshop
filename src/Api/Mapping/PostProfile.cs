using Api.DTOs;
using Api.Entities;
using AutoMapper;

namespace Api.Mapping
{
	public class PostProfile : Profile
	{
		public PostProfile()
		{
			CreateMap<AddPostDTO, Post>().ReverseMap();
		}
	}
}
