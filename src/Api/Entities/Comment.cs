namespace Api.Entities
{
	public class Comment
	{
		public int Id { get; set; }
		public string Content { get; set; } = null!;
		public string Author { get; set; } = null!;
		public int PostId { get; set; }
		public virtual Post Post { get; set; } = null!;
	}
}
