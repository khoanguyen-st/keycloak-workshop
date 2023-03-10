namespace Api.DTOs
{
	public class GetProfileDTO
	{
		// Create a DTO with an object like this
		// {"id":"bf4e6b6a-d7cc-4f66-bd7e-91dc23231ed0","createdTimestamp":1678091461247,"username":"khoa","enabled":true,"totp":false,"emailVerified":false,"firstName":"Khoa","lastName":"Nguyen","email":"khoa@gmail.com","attributes":{"permission":["create-user"],"age":["18"]},"disableableCredentialTypes":[],"requiredActions":[],"notBefore":0,"access":{"manageGroupMembership":true,"view":true,"mapRoles":true,"impersonate":false,"manage":true}}
		public string Id { get; set; } = null!;
		public long CreatedTimestamp { get; set; }
		public string Username { get; set; } = null!;
		public bool Enabled { get; set; }
		public bool Totp { get; set; }
		public bool EmailVerified { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public Dictionary<string, string[]>? Attributes { get; set; }
		public string[]? DisableableCredentialTypes { get; set; }
		public string[]? RequiredActions { get; set; }
		public int NotBefore { get; set; }
		public AccessClass Access { get; set; } = new AccessClass();

		public class AccessClass
		{
			public bool ManageGroupMembership { get; set; }
			public bool View { get; set; }
			public bool MapRoles { get; set; }
			public bool Impersonate { get; set; }
			public bool Manage { get; set; }
		}
	}
}
