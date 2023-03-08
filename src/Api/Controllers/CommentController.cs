using Api.Context;
using Api.DTOs;
using Api.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly KeycloakContext _context;
		private readonly IMapper _mapper;

		public CommentController(KeycloakContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _context.Comment.Include(c => c.Post).ToListAsync());
		}

		//Get Comment by Id
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var comment = await _context.Comment.Include(c => c.Post).FirstOrDefaultAsync(c => c.Id == id);

			if (comment == null)
			{
				return NotFound("This comment does not exist!");
			}

			return Ok(comment);
		}

		//Method POST to add Comment instance to database
		[HttpPost]
		public async Task<IActionResult> Post(AddCommentDTO addComment)
		{
			var comment = _mapper.Map<Comment>(addComment);

			var authorId = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

			comment.Author = authorId.Value;

			_context.Comment.Add(comment);

			await _context.SaveChangesAsync();

			return Ok(comment);
		}

		//Method PUT to update Comment instance to database
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, AddCommentDTO comment)
		{
			var existtedComment = await _context.Comment.FindAsync(id);

			if (existtedComment == null)
			{
				return BadRequest("This comment does not exist!");
			}

			_mapper.Map(comment, existtedComment);

			await _context.SaveChangesAsync();

			return Ok(existtedComment);
		}

		//Method DELETE to delete Comment instance from database
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var comment = await _context.Comment.FindAsync(id);

			if (comment == null)
			{
				return BadRequest("This comment does not exist!");
			}

			_context.Comment.Remove(comment);

			await _context.SaveChangesAsync();

			return Ok();
		}
    }
}
