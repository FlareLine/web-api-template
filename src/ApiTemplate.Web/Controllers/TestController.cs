using System;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Web.Controllers
{
	/// <summary>
	/// Test controller for application.
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class TestController
	{
		[HttpGet]
		public IActionResult ServerError()
		{
			throw new ApplicationException("This endpoint should throw a 500 server error.");
		}
	}
}
