namespace Api.DTOs
{
	public class AddPermissionDTO
	{
		public string UserId { get; set; } = null!;
		public List<string> Permissions { get; set; } = new List<string>();
	}
}
