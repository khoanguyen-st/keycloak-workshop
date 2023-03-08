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
		[Authorize(Policy = "Author")]
		public IActionResult Get()
		{
			foreach(var claim in User.Claims)
			{
				if(claim.Type == "resource_access")
				{
                    var rsAccess = JsonConvert.DeserializeObject<JObject>(claim.Value);

					var roleList = rsAccess.SelectToken("my-app.roles").ToList();

					var strList = new List<string>();

					foreach (var item in roleList)
					{
						strList.Add(item.ToString());
					}

					Console.WriteLine(JsonConvert.SerializeObject(strList));
                }
			}

			return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
		}
	}
}
