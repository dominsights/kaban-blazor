using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Net;
using Kanban.Services;

// Credits to Jason Watmore: https://jasonwatmore.com/post/2020/09/27/blazor-webassembly-authentication-without-identity

namespace Kanban.Components
{
    public class AppRouteView : RouteView
    {
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public AuthenticationService? AuthenticationService { get; set; }

        protected override void Render(RenderTreeBuilder builder)
        {
            var authorize = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) != null;
            bool isLoggedIn = AuthenticationService != null ? AuthenticationService.IsLoggedIn : false;

            if (authorize && !isLoggedIn)
            {
                var returnUrl = WebUtility.UrlEncode(new Uri(NavigationManager != null ? NavigationManager.Uri : "").PathAndQuery);
                NavigationManager?.NavigateTo($"login?returnUrl={returnUrl}");
            }
            else
            {
                base.Render(builder);
            }
        }
    }
}