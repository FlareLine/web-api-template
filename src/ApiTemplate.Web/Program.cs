using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ApiTemplate.Web
{
	/// <summary>
	/// Program entry-point.
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// Program entry-point.
		/// </summary>
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		/// <summary>
		/// Create the <see cref="IWebHostBuilder"/> for the program.
		/// </summary>
		/// <param name="args">Program arguments.</param>
		/// <returns><see cref="IWebHostBuilder"/> instance.</returns>
		private static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost
				.CreateDefaultBuilder(args)
				.UseKestrel()
				.UseStartup<Startup>();
		}
	}
}
