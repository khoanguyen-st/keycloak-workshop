using Api.DTOs;
using Api.Entities;
using AutoMapper;

namespace Api.Mapping
{
	public class CommentProfile : Profile
	{
		public CommentProfile()
		{
			CreateMap<AddCommentDTO, Comment>().ReverseMap();
		}
	}
}
