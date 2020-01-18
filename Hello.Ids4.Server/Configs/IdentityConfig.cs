using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.IdentityServer.Configs
{
    public class IdentityConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                // OpenID Connect隐式流客户端 
                new Client
                {
                    ClientId = "Implicit",
                    ClientName = "vue shop",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    //RequireConsent=true,//如果不需要显示否同意授权 页面 这里就设置为false
                    RedirectUris = { "http://localhost:9192/callback" },//登录成功后返回的客户端地址
                    PostLogoutRedirectUris = { "http://localhost:9192/index" },//注销登录后返回的客户端地址 
                    AllowedScopes =//下面这两个必须要加吧 不太明白啥意思
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowAccessTokensViaBrowser = true,
                    AllowedCorsOrigins =
                    {
                        "http://localhost:9192"
                    }
                },
                //用户身份验证 需要用户登录 会自动跳转到ids4的登录页面
                //客户端调用 await HttpContext.AuthenticateAsync() 可以获取授权属性，id_token 等信息
                //直接访问：http://localhost:5001/home/privacy
                 new Client
                {
                    ClientId = "Implicit.MVC",
                    ClientName = "MVC shop",
                    AllowedGrantTypes = GrantTypes.Implicit,
                  //  RequireConsent=false,//如果不需要显示否同意授权 页面 这里就设置为false
                    RedirectUris = { "http://localhost:5001/signin-oidc" },//登录成功后返回的客户端地址
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },//注销登录后返回的客户端地址
                    AllowedScopes =//下面这两个必须要加吧 不太明白啥意思
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    //AllowAccessTokensViaBrowser = true,
                    //AllowedCorsOrigins =
                    //{
                    //    "http://localhost:5001"
                    //}
                },
                //客户端授权 不需要用户登录， 
                //post 请求 { client_id:'',grant_type:'client_credentials',client_secret:''}
                //直接访问：http://localhost:5001/identity
                 new Client
                {
                    ClientId = "Client.MVC",
                    ClientName = "MVC shop",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RequireConsent=false,//如果不需要显示否同意授权 页面 这里就设置为false
                    RedirectUris = { "http://localhost:5001/signin-oidc" },//登录成功后返回的客户端地址
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },//注销登录后返回的客户端地址
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =//下面这两个必须要加吧 不太明白啥意思
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },
                 new Client
                {
                    ClientId = "demo_api_swagger",
                    ClientName = "Swagger UI for demo_api",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser  =true,
                    //RequireConsent=true,//如果不需要显示否同意授权 页面 这里就设置为false
                    RedirectUris = { "http://localhost:8090/swagger/oauth2-reidrect.html" },//登录成功后返回的客户端地址 
                    AllowedScopes ={"demo_api"} 
                },
                //混合模式
                new Client
                {
                    ClientId = "Hybrid.MVC",
                    ClientName = "vue shop",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    //RequireConsent=true,//如果不需要显示否同意授权 页面 这里就设置为false
                    RedirectUris = { "http://localhost:5001/signin-oidc" },//登录成功后返回的客户端地址
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },//注销登录后返回的客户端地址
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =//下面这两个必须要加吧 不太明白啥意思
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                }, 
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("api1","api项目一")
                {
                   // ApiSecrets = { new Secret("api1pwd".Sha256()) }
                },
                new ApiResource("demo_api","Demo API with Swagger")
            };
        }
    }
}
