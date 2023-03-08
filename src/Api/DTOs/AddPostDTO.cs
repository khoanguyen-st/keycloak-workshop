namespace Api.DTOs
{
	public class AddPostDTO
	{
		public string Title { get; set; } = null!;
		public string Content { get; set; } = null!;
		public string Type { get; set; } = "Public";
	}
}
