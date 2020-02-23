using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiTemplate.Web
{
	/// <summary>
	/// Class to set up application services and configuration.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Create a new <see cref="Startup"/> instance with the specified configuration.
		/// </summary>
		/// <param name="configuration">Configuration instance.</param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Application configuration.
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Set up any required service configuration.
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/> for configured services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddMvc(options => options.EnableEndpointRouting = false)
				.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
		}

		/// <summary>
		/// Method to configure HTTP request pipeline.
		/// </summary>
		/// <param name="app"><see cref="IApplicationBuilder"/> instance.</param>
		/// <param name="env"><see cref="IWebHostEnvironment"/> instance.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseExceptionHandler(new ExceptionHandlerOptions
			{
				ExceptionHandler = HandleException
			});

			app.UseMvc();
		}

		private static async Task HandleException(HttpContext context)
		{
			context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

			var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>()?.Error;

			if (exceptionHandler == null)
			{
				return;
			}

			var error = new
			{
				message = exceptionHandler.Message
			};

			context.Response.ContentType = "application/json";

			await using var writer = new StreamWriter(context.Response.Body);
			await writer.WriteAsync(JsonSerializer.Serialize(error));
			await writer.FlushAsync().ConfigureAwait(false);
		}
	}
}
