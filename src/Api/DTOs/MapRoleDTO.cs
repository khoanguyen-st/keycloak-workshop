using Newtonsoft.Json;

namespace Api.DTOs
{
	public class MapRoleDTO
	{
		public class RoleObject
		{
			[JsonProperty("id")]
			public string Id { get; set; } = null!;
			[JsonProperty("name")]
			public string Name { get; set; } = null!;
			[JsonProperty("composite")]
			public bool Composite { get; set; } = false;
			[JsonProperty("clientRole")]
			public bool ClientRole { get; set; } = true;
			[JsonProperty("containerId")]
			public string ContainerId { get; set; } = null!;

		}
		public string UserId { get; set; } = null!;
		public List<RoleObject> ListRole { get; set; } = new List<RoleObject>();
	}
}
