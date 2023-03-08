using System.Text.Json.Serialization;

namespace Api.DTOs
{
	public class ResourceAccessDTO
	{
		public class AccountResource
		{
			[JsonPropertyName("roles")]
			public List<string> Roles { get; set; } = new List<string>();
		}

		public class MyAppResource
		{
			[JsonPropertyName("roles")]
			public List<string> Roles { get; set; } = new List<string>();
		}

		[JsonPropertyName("account")]
		public AccountResource account { get; set; } = null!;

		[JsonPropertyName("my-app")]
		public MyAppResource MyApp { get; set; } = null!;
	}
}
