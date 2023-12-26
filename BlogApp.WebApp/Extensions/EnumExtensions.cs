using System.ComponentModel;
using BlogApp.Models;

namespace BlogApp.WebApp.Extensions;

public static class EnumRoleExtensions
{
    public static string ToRoleDescriptionString(this EUserRole val)
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])val
            .GetType()!
            .GetField(val.ToString())!
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }
}
