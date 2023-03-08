using Api.Context;
using Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly KeycloakContext _context;
		private readonly IMapper _mapper;
		private static HttpClient client = new HttpClient();
		private readonly string rootPath = "http://localhost:8080";
		private readonly string realm = "my-realm";
		private readonly string clientId = "e5a3508e-70c1-4084-9365-a5935275e998";

		public UserController(KeycloakContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetList()
		{
			var token = HttpContext.GetTokenAsync("access_token").Result;

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var responseResult = await client.GetAsync($"{rootPath}/admin/realms/{realm}/users");

			if (!responseResult.IsSuccessStatusCode)
			{
				return BadRequest(await responseResult.Content.ReadAsStringAsync());
			}

			return Ok(await responseResult.Content.ReadAsStringAsync());
		}

		//Get user keycloak by id
		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(string id)
		{
			var token = HttpContext.GetTokenAsync("access_token").Result;

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var responseResult = await client.GetAsync($"{rootPath}/admin/realms/{realm}/users/{id}");

			if (!responseResult.IsSuccessStatusCode)
			{
				return BadRequest(await responseResult.Content.ReadAsStringAsync());
			}

			return Ok(await responseResult.Content.ReadAsStringAsync());
		}

		[HttpPost]
		public async Task<IActionResult> Create(AddUserDTO addUserDTO)
		{
			try
			{
				var token = HttpContext.GetTokenAsync("access_token").Result;

				var userKeycloak = new
				{
					username = addUserDTO.UserName,
					firstName = addUserDTO.FirstName,
					lastName = addUserDTO.LastName,
					email = addUserDTO.Email,
					enabled = true,
					credentials = new List<object>
				{
					new
					{
						type = "password",
						value = addUserDTO.Password,
						temporary = false
					}
				}
				};

				var json = JsonConvert.SerializeObject(userKeycloak);

				var content = new StringContent(json, Encoding.UTF8, "application/json");

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var responseResult = await client.PostAsync($"{rootPath}/admin/realms/{realm}/users", content);

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
