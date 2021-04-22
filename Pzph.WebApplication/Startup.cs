using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Pzph.RepositoryLayer;
using Pzph.RepositoryLayer.Repositories;
using Pzph.ServiceLayer.Bookings.Services;
using Pzph.ServiceLayer.Common;
using Pzph.ServiceLayer.Users.Domain;
using Pzph.ServiceLayer.Users.Models;
using Pzph.ServiceLayer.Users.Services;
using Pzph.WebApplication.Config;

namespace Pzph.WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IdentityServerConfig>(Configuration.GetSection("IdentityServer"));

            services.AddDbContext<PzphDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Default")).UseLazyLoadingProxies());

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<PzphDbContext>()
                .AddDefaultTokenProviders();

            AddAuth(services);

            services.AddControllers();

            services.AddAutoMapper(config => config.AddProfile(typeof(PzphProfile)));

            services.AddScoped<IUsersService, UsersService>();

            services.AddScoped<IBookingsRepository, BookingsRepository>();
            services.AddScoped<IBookingsService, BookingsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<PzphDbContext>();
            db.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseHttpsRedirection();

            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void AddAuth(IServiceCollection services)
        {
            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/Identity/Account/Login";
                    options.UserInteraction.LogoutUrl = "/Identity/Account/Logout";
                })
                .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                .AddInMemoryApiScopes(IdentityConfig.GetApiScopes())
                .AddInMemoryClients(IdentityConfig.GetClients(Configuration))
                .AddInMemoryPersistedGrants()
                .AddAspNetIdentity<User>()
                .AddDeveloperSigningCredential();

            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = Configuration.GetSection("IdentityServer").GetValue<string>("AuthorityUrl");
                    options.RequireHttpsMetadata = true;
                    options.Audience = "pzph.api";
                    options.SaveToken = true;
                });

            services.AddAuthorization(settings =>
            {
                settings.AddPolicy(
                    "user",
                    policy => policy.RequireAuthenticatedUser().RequireClaim("scope", "pzph.api"));
            });
        }
    }
}