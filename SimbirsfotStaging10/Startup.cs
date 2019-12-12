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

namespace SimbirsfotStaging10
{
	public class Startup
	{
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
            services.AddTransient<IPlatformsService, PlatformsServices>();

            services.AddDbContextPool<SkiDBContext>( optionsBuilder => 
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUserService, UserService>();
            services.AddIdentity<User, CustomRole>(opts => opts.SetCustomIdentityOptions())
                .AddEntityFrameworkStores<SkiDBContext>();
            services.ConfigureApplicationCookie( opts => opts.LoginPath = "/Account/Login");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
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
