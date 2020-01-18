using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace Hello.MvcClient
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


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region 配置认证中心ids4的及自己作为客户端的认证信息
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc"; 
            })
            .AddCookie("Cookies")

            #region GrantTypes.Hybrid 混合模式
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";
                options.Authority = "http://localhost:5000"; //认证服务器地址
                options.RequireHttpsMetadata = false;

                options.ClientId = "Hybrid.MVC";
                options.ClientSecret = "secret";
                options.ResponseType = "code id_token";

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.Scope.Add("api1");
                options.Scope.Add("offline_access");
                options.ClaimActions.MapJsonKey("website","website");
            })
            #endregion
            #region GrantTypes.Implicit 隐式许可
            //.AddOpenIdConnect("oidc", options =>
            //{
            //    options.Authority = "http://localhost:5000"; //认证服务器地址
            //    options.RequireHttpsMetadata = false;
            //    options.ClientId = "Implicit.MVC";
            //    options.SaveTokens = true;
            //})
            #endregion
            #region GrantTypes.ClientCredentials 客户端模式
            //.AddOpenIdConnect("oidc", options =>
            //{
            //    options.Authority = "http://localhost:5000"; //认证服务器地址
            //    options.RequireHttpsMetadata = false;
            //    options.ClientId = "Client.MVC";
            //    options.ClientSecret = "secret";
            //    options.SaveTokens = true;
            //})
            #endregion
            ;
            IdentityModelEventSource.ShowPII = true;
            #endregion

            #region jwt
            //services.AddAuthentication("Bearer")
            //        .AddJwtBearer("Bearer", options =>
            //        {
            //            options.Authority = "http://localhost:5000";
            //            options.RequireHttpsMetadata = false;
            //            options.Audience = "mvc";
            //        });
            #endregion
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();
            //app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
