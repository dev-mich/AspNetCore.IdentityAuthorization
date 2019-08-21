using AspNetCore.IdentityAuthorization.Extensions;
using AspNetCore.IdentityAuthorization.Requirements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.IdentityAuthorization.TestApi
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
            services.AddIdentityAuthorization(opts =>
            {
                opts.Authority = "https://identitystaging.udressy.com";
            });

            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(opts =>
            {
                opts.Authority = "https://identitystaging.udressy.com";
                opts.ApiName = "project_api";
                opts.ApiSecret = "project_api";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(EmailVerifiedRequirement.PolicyName, bld =>
                {
                    bld.Requirements.Add(new EmailVerifiedRequirement());
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
