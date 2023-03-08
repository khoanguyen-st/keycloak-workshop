using Newtonsoft.Json;

namespace Api.DTOs
{
	public class AddRoleDTO
	{
		[JsonProperty("name")]
		public string Name { get; set; } = null!;
		[JsonProperty("composite")]
		public bool Composite { get; set; } = false;
		[JsonProperty("clientRole")]
		public bool ClientRole { get; set; } = true;
	}
}
