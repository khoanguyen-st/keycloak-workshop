namespace Api.DTOs
{
	public class GetTokenDTO
	{
		// Create a DTO with an object like this
		//"access_token": "",
		//"expires_in": 3600,
		//"refresh_expires_in": 1800,
		//"refresh_token": "",
		//"token_type": "Bearer",
		//"not-before-policy": 0,
		//"session_state": "66f0e67f-5b2d-4a52-a0e2-b64df40725db",
		//"scope": "sex profile custom-client-scope DOB permission email"
		public string Access_token { get; set; } = null!;
		public int Expires_in { get; set; }
		public int Refresh_expires_in { get; set; }
		public string Refresh_token { get; set; } = null!;
		public string? Token_type { get; set; }
		public int Not_before_policy { get; set; }
		public string? Session_state { get; set; }
		public string? Scope { get; set; }
	}
}
