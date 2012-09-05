using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;

namespace RestSharp.Extensions
{
    internal static class HttpWebResponseExtensions
    {
        public static void Close(this HttpWebResponse response)
        {
            response.Dispose();
        }
    }

    internal static class AssemblyExtensions
    {
        public static string GetVersion()
        {
            var asmName = typeof(AssemblyExtensions).AssemblyQualifiedName;
            var versionExpression = new System.Text.RegularExpressions.Regex("Version=(?<version>[0-9.]*)");
            var m = versionExpression.Match(asmName);
            if (m.Success)
            {
                return "WinRT " + m.Groups["version"].Value;
            }

            return null;
        }
    }

    internal static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetProperties(this Type type)
        {
            return type.GetRuntimeProperties();
        }

        public static Type[] GetGenericArguments(this Type type)
        {
            return type.GenericTypeArguments;
        }

        public static Type[] GetInterfaces(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces.ToArray();
        }

        public static bool IsGenericType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }

        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        public static bool IsPrimitive(this Type type)
        {
            return type.GetTypeInfo().IsPrimitive;
        }

        public static Type BaseType(this Type type)
        {
            return type.GetTypeInfo().BaseType;
        }

        public static bool IsPublic(this Type type)
        {
            return type.GetTypeInfo().IsPublic;
        }

        public static bool IsValueType(this Type type)
        {
            return type.GetTypeInfo().IsValueType;
        }

        public static bool IsInstanceOfType(this Type type, object o)
        {
            return o != null && type.IsAssignableFrom(o.GetType());
        }

        internal static bool ImplementInterface(this Type type, Type ifaceType)
        {
            while (type != null)
            {
                Type[] interfaces = type.GetInterfaces();
                if (interfaces.Any())
                {
                    if (interfaces.Any(t => t == ifaceType || (t != null && t.ImplementInterface(ifaceType))))
                    {
                        return true;
                    }
                }

                // type = type.BaseType;
                type = type.GetTypeInfo().BaseType;
            }
            return false;
        }

        public static bool IsAssignableFrom(this Type type, Type c)
        {
            if (c == null)
            {
                return false;
            }
            if (type == c)
            {
                return true;
            }

            //RuntimeType runtimeType = type.UnderlyingSystemType as RuntimeType;
            //if (runtimeType != null)
            //{
            //    return runtimeType.IsAssignableFrom(c);
            //}

            //if (c.IsSubclassOf(type))
            if (c.GetTypeInfo().IsSubclassOf(c))
            {
                return true;
            }

            //if (type.IsInterface)
            if (type.GetTypeInfo().IsInterface)
            {
                return c.ImplementInterface(type);
            }

            if (type.IsGenericParameter)
            {
                Type[] genericParameterConstraints = type.GetTypeInfo().GetGenericParameterConstraints();
                for (int i = 0; i < genericParameterConstraints.Length; i++)
                {
                    if (!genericParameterConstraints[i].IsAssignableFrom(c))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }

    internal static class ExtraStringExtensions
    {
        public static string ToLower(this string text, CultureInfo cultureInfo)
        {
            return text.ToLower(); // Or should use ToLowerInvariant?
        }

        public static string ToUpper(this string text, CultureInfo cultureInfo)
        {
            return text.ToUpper(); // Or should use ToUpperInvariant?
        }
    }

    internal static class CharExtensions
    {
        public static char ToLower(char c, CultureInfo culture)
        {
            return char.ToLower(c); // Or should use ToUpperInvariant?
        }

        public static char ToUpper(char c, CultureInfo culture)
        {
            return char.ToUpper(c); // Or should use ToUpperInvariant?
        }
    }
}