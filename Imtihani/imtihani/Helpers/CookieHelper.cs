using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;

namespace Imtihani.Helpers
{
    public static class CookieHelper
    {
        public static void SetCookie(this HttpContext context, string key, string value, DateTime? period = null)
        {
            //CookieOptions options = new CookieOptions() { SameSite = SameSiteMode.Strict, Expires = period.HasValue ? period.Value : DateTime.Now.AddMinutes(30), HttpOnly = true, Domain = context.Request.Host.Host, IsEssential = true, Secure = true };
            CookieOptions options = new CookieOptions() { SameSite = SameSiteMode.Strict, Expires = period.HasValue ? period.Value : DateTime.Now.AddMinutes(30), HttpOnly = false, Domain = context.Request.Host.Host, IsEssential = true, Secure = true };
            context.Response.Cookies.Append(key, value, options);
        }

        public static string GetCookie(this HttpContext context, string key)
        {
            string cookie = context.Request.Cookies[key];
            if (cookie != null)
            {
                return cookie;
            }
            return "";
        }
    }
}
