using Api.Entities;

namespace Api.DTOs
{
	public class AddCommentDTO
	{
		public string Content { get; set; } = null!;
		public string Author { get; set; } = null!;
		public int PostId { get; set; }
	}
}
