using Api.DTOs;
using Api.Policy.PermissionPolicy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private static HttpClient client = new HttpClient();
		private readonly string rootPath = "http://localhost:8080";
		private readonly string realm = "my-realm";
		private readonly string clientId = "e5a3508e-70c1-4084-9365-a5935275e998";

		//Get all role of a client in keycloak
		[HttpGet()]
		[PermissionAuthorize("view-role")]
		public async Task<IActionResult> GetRole()
		{
			var token = HttpContext.GetTokenAsync("access_token").Result;

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var responseResult = await client.GetAsync($"{rootPath}/admin/realms/{realm}/clients/{clientId}/roles");

			if (!responseResult.IsSuccessStatusCode)
			{
				return BadRequest(await responseResult.Content.ReadAsStringAsync());
			}

			return Ok(await responseResult.Content.ReadAsStringAsync());
		}

		[HttpPost()]
		[PermissionAuthorize("create-role")]
		public async Task<IActionResult> CreateRole(AddRoleDTO addRoleDTO)
		{
			var token = HttpContext.GetTokenAsync("access_token").Result;

			var json = JsonConvert.SerializeObject(addRoleDTO);

			var content = new StringContent(json, Encoding.UTF8, "application/json");

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var responseResult = await client.PostAsync($"{rootPath}/admin/realms/{realm}/clients/{clientId}/roles", content);

			if (!responseResult.IsSuccessStatusCode)
			{
				return BadRequest(await responseResult.Content.ReadAsStringAsync());
			}

			return Ok(await responseResult.Content.ReadAsStringAsync());
		}

		//Create a method to Add client-level roles to the user role mapping keycloak
		[HttpPost("Mapping")]
		[PermissionAuthorize("map-role")]
		public async Task<IActionResult> CreateRoleMapping(MapRoleDTO listRole)
		{
			var token = HttpContext.GetTokenAsync("access_token").Result;

			var json = JsonConvert.SerializeObject(listRole.ListRole);

			var content = new StringContent(json, Encoding.UTF8, "application/json");

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var responseResult = await client.PostAsync($"{rootPath}/admin/realms/{realm}/users/{listRole.UserId}/role-mappings/clients/{clientId}", content);

			if (!responseResult.IsSuccessStatusCode)
			{
				return BadRequest(await responseResult.Content.ReadAsStringAsync());
			}

			return Ok(await responseResult.Content.ReadAsStringAsync());
		}
	}
}
