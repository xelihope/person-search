using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PersonSearch.Helpers
{
    public static class EnumExtensions
    {
        public static string ToDisplay(this Enum enumObj)
        {
            var enumAttr = enumObj.GetType().GetMember(enumObj.ToString())[0].GetCustomAttribute<DisplayAttribute>();
            if (enumAttr != null) {
                return enumAttr.GetName();
            }
            return enumObj.ToString();
        }
    }
}