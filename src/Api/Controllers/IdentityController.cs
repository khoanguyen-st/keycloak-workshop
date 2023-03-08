using Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
	[Route("identity")]
	[Authorize]
	[ApiController]
	public class IdentityController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			var permissions = User.Claims.Where(c => c.Type == "permission").Select(c => c.Value).ToList();

            Console.WriteLine(JsonConvert.SerializeObject(permissions, new JsonSerializerSettings()
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			}));

            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
		}
	}
}
