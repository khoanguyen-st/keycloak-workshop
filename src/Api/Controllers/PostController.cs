using Api.Context;
using Api.DTOs;
using Api.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly KeycloakContext _context;
		private readonly IMapper _mapper;
		private readonly IAuthorizationService _authorizationService;

		public PostController(KeycloakContext context, IMapper mapper, IAuthorizationService authorizationService)
		{
			_context = context;
			_mapper = mapper;
			_authorizationService = authorizationService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _context.Post.Include(p => p.Comments).ToListAsync());
		}

		[HttpGet("AdultOnly")]
		[Authorize(Policy = "AdultOnly")]
		public async Task<IActionResult> GetAdultOnly()
		{
			var posts = await _context.Post.Include(p => p.Comments).ToListAsync();

			var adultOnlyPosts = posts.Where(p => p.Type == "Adult Only");

			return Ok(adultOnlyPosts);
		}

		[HttpGet("FemaleOnly")]
		[Authorize(Policy = "FemaleOnly")]
		public async Task<IActionResult> GetFemaleOnly()
		{
			var posts = await _context.Post.Include(p => p.Comments).ToListAsync();

			var femaleOnlyPosts = posts.Where(p => p.Type == "Female Only");

			return Ok(femaleOnlyPosts);
		}


		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Post(AddPostDTO addPost)
		{
			var post = _mapper.Map<Post>(addPost);

			var authorId = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

			post.Author = authorId.Value;

			_context.Post.Add(post);

			await _context.SaveChangesAsync();

			return Ok(post);
		}

		//Method PUT to update Post instance to database
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, AddPostDTO post)
		{
			var existtedPost = await _context.Post.FindAsync(id);

			if (existtedPost == null)
			{
				return BadRequest("This post does not exist!");
			}

			_mapper.Map(post, existtedPost);

			_context.Entry(existtedPost).State = EntityState.Modified;

			await _context.SaveChangesAsync();

			return Ok(existtedPost);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var post = await _context.Post.FindAsync(id);

			if (post == null)
			{
				return NotFound();
			}

			if((await _authorizationService.AuthorizeAsync(User, post.Author, "PostAuthorOnly")).Succeeded == false)
			{
				return Forbid();
			}

			_context.Post.Remove(post);

			await _context.SaveChangesAsync();

			return Ok(post);
		}

    }
}
