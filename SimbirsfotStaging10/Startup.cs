using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimbirsfotStaging10.DAL.Data;
using Microsoft.EntityFrameworkCore;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.BLL.Services;
using SimbirsfotStaging10.DAL.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SimbirsfotStaging10.BLL.VK;
using SimbirsfotStaging10.Controllers;
using SimbirsfotStaging10.Logger;


namespace SimbirsfotStaging10
{
	public class Startup
	{
        private readonly Queue<EventLog> queueForLogs = new Queue<EventLog>();


		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{           
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
    
            services.AddDbContextPool<SkiDBContext>( optionsBuilder => 
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICardService, CardService>();
            services.AddIdentity<User, CustomRole>(opts => opts.SetCustomIdentityOptions())
                .AddEntityFrameworkStores<SkiDBContext>();
            services.ConfigureApplicationCookie( opts => opts.LoginPath = "/Account/Login");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddQuartzDbLogging();
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                opts.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
                opts.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
            });
            services.AddSingleton<VkAuth>();
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory, IApplicationLifetime lifetime)
		{            
            factory.AddQueueLog( (cat, lvl) => cat.StartsWith("SimbirsfotStaging10"),
                queueToSetLogs: queueForLogs);
            app.StartJob(lifetime,Configuration, queueForLogs);

            if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseMvc(routes => routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
		}
	}
}
