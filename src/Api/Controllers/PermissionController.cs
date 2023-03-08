using Api.Context;
using Api.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PermissionController : ControllerBase
	{
		private readonly KeycloakContext _context;
		private static HttpClient client = new HttpClient();
		private readonly string rootPath = "http://localhost:8080";
		private readonly string realm = "my-realm";
		private readonly string clientId = "e5a3508e-70c1-4084-9365-a5935275e998";

		public PermissionController(KeycloakContext context)
        {
			_context = context;
		}

        [HttpGet]
		public async Task<IActionResult> GetList()
		{
			return Ok(await _context.Permission.ToListAsync());
		}

		//Post method to add permission to permission attribute by updating user in keycloak
		[HttpPost("Permission")]
		public async Task<IActionResult> AddPermission(AddPermissionDTO addPermissionDTO)
		{
			try
			{
				var token = HttpContext.GetTokenAsync("access_token").Result;

				var userKeycloak = new
				{
					attributes = new
					{
						permission = addPermissionDTO.Permissions
					}
				};

				var json = JsonConvert.SerializeObject(userKeycloak);

				var content = new StringContent(json, Encoding.UTF8, "application/json");

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var responseResult = await client.PutAsync($"{rootPath}/admin/realms/{realm}/users/{addPermissionDTO.UserId}", content);

				if (!responseResult.IsSuccessStatusCode)
				{
					return BadRequest(await responseResult.Content.ReadAsStringAsync());
				}

				return Ok(await responseResult.Content.ReadAsStringAsync());
			}
			catch (Exception e)
			{
				return BadRequest(new
				{
					error = e.Message
				});
			}
		}
	}
}
