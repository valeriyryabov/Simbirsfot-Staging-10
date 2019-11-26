using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SimbirsfotStaging10.Logger;
using Microsoft.Extensions.Logging;

namespace SimbirsfotStaging10
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging( (cont,builder ) =>
                {
                    builder.AddEventLog();
                    builder.AddConfiguration(cont.Configuration.GetSection("Logging"));
                })
				.UseStartup<Startup>();
	}
}
