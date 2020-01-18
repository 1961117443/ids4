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
                // OpenID Connect隐式流客户端（MVC）
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    //RequireConsent=true,//如果不需要显示否同意授权 页面 这里就设置为false
                    RedirectUris = { "http://localhost:5001/signin-oidc" },//登录成功后返回的客户端地址
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },//注销登录后返回的客户端地址
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =//下面这两个必须要加吧 不太明白啥意思
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new Client
                {
                    ClientId = "mvc1",
                    ClientName = "MVC Client",
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
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new Client
                {
                    ClientId = "mvc2",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireConsent=false,//如果不需要显示否同意授权 页面 这里就设置为false
                    RedirectUris = { "http://localhost:5001/signin-oidc" },//登录成功后返回的客户端地址
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },//注销登录后返回的客户端地址
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =//下面这两个必须要加吧 不太明白啥意思
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}
