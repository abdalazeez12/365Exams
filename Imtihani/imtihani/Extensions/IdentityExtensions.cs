using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using static IbtecarFramework.Enums;

public static class IdentityExtensions
{
    public static bool IsTeacher(this IIdentity identity)
    {
        return identity.GetUserType() == (int)UserTypes.SpecialUser;
    }

    public static bool IsStudent(this IIdentity identity)
    {
        var userType = identity.GetUserType();
        return userType == (int)UserTypes.User || userType == (int)UserTypes.SystemAdmin || userType == (int)UserTypes.Admin;
    }

    public static bool IsAdmin(this IIdentity identity)
    {
        var userType = identity.GetUserType();
        return userType == (int)UserTypes.Admin || userType == (int)UserTypes.SystemAdmin;
    }

    public static int GetUserType(this IIdentity identity)
    {
        if (!(identity is ClaimsIdentity))
            return 0;
        var claim = (identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == "UserType");
        if (claim != null && !string.IsNullOrEmpty(claim.Value))
            return Convert.ToInt32(claim.Value);
        return 0;
    }

    public static string GetUserEmail(this IIdentity identity)
    {
        if (!(identity is ClaimsIdentity))
            return "";
        var claim = (identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (claim != null && !string.IsNullOrEmpty(claim.Value))
            return claim.Value;
        return "";
    }
}