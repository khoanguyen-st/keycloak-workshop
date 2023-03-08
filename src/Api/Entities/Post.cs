namespace Api.Entities
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string Content { get; set; } = null!;
		public string Author { get; set; } = null!;
		public string Type { get; set; } = "Public";
		public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
	}
}
