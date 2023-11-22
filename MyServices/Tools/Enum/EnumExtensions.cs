using System.ComponentModel;
using System.Reflection;

namespace MyServices.Tools.EnumHelper
{
    public static class EnumExtensions
        {
            public static string ToDescription(this Enum value)
            {
                FieldInfo field;
                DescriptionAttribute attribute;
                string result;

                field = value.GetType().GetField(value.ToString());
                attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                result = attribute != null ? attribute.Description : string.Empty;

                return result;
            }

            public static bool HasDescription(this Enum someEnum)
            {
                return !string.IsNullOrEmpty(someEnum.ToDescription());
            }

            public static string ToDisplayName(this Enum value)
            {
                FieldInfo field;
                DisplayNameAttribute attribute;
                string result;

                field = value.GetType().GetField(value.ToString());
                attribute = (DisplayNameAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayNameAttribute));
                result = attribute != null ? attribute.DisplayName : string.Empty;

                return result;
            }

            public static bool HasDisplayName(this Enum someEnum)
            {
                return !string.IsNullOrEmpty(someEnum.ToDisplayName());
            }

            public static int ToValue(this Enum someEnum)
            {
                return Convert.ToInt32(someEnum);
            }

        }
    }
