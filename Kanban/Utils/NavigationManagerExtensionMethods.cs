using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Specialized;
using System.Web;

// Credits to Jason Watmore: https://jasonwatmore.com/post/2020/08/09/blazor-webassembly-get-query-string-parameters-with-navigation-manager

namespace Kanban
{
        public static class ExtensionMethods
    {
        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        public static string QueryString(this NavigationManager navigationManager, string key)
        {
            var queryString = navigationManager.QueryString()[key];
            return queryString ?? "";
        }
    }
}