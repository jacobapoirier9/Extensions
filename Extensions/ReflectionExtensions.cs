using Extensions.Attributes;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Extensions
{
    /// <summary>
    /// A collection of reflection extension methods
    /// </summary>
    public static class ReflectionExtensions
    {
        #region Attributes
        /// <summary>
        /// Gets an attribute from a member
        /// </summary>
        public static T Get<T>(this MemberInfo info) where T : Attribute
        {
            return info.DeclaringType.Get<T>(info.Name);
        }

        /// <summary>
        /// Gets a class level attribute of an instance
        /// </summary>
        public static T Get<T>(this object instance) where T : Attribute =>
            instance.GetType().Get<T>();


        /// <summary>
        /// Gets a class level attribute of a type
        /// </summary>
        public static T Get<T>(this Type type) where T : Attribute
        {
            foreach (var data in type.GetCustomAttributes())
            {
                try { return (T)(object)(data); }
                catch { }
            }

            return default;
        }

        /// <summary>
        /// Gets an attribute from a member
        /// </summary>
        public static T Get<T>(this object instance, string member) where T : Attribute =>
            instance.GetType().Get<T>(member);

        /// <summary>
        /// Gets an attribute from a member
        /// </summary>
        public static T Get<T>(this Type type, string member) where T : Attribute
        {
            foreach (var info in type.GetMembers())
            {
                if (info.Name.ToLower() == member.ToLower())
                {
                    foreach (var data in info.GetCustomAttributes())
                    {
                        try { return (T)(object)(data); }
                        catch { }
                    }
                }
            }

            return default;
        }
        #endregion



        #region Methods
        /// <summary>
        /// Gets a list of members that match a predicate
        /// </summary>
        public static List<MemberInfo> GetMembers<T>(this T instance, Func<MemberInfo, bool> predicate)
        {
            return typeof(T).GetMembers(predicate);
        }

        /// <summary>
        /// Gets a list of methods that match a predicate
        /// </summary>
        public static List<MethodInfo> GetMethods<T>(this T instance, Func<MethodInfo, bool> predicate)
        {
            return typeof(T).GetMethods(predicate);
        }

        /// <summary>
        /// Gets a list of properties that match a predicate
        /// </summary>
        public static List<PropertyInfo> GetProperties<T>(this T instance, Func<PropertyInfo, bool> predicate)
        {
            return typeof(T).GetProperties(predicate);
        }

        /// <summary>
        /// Gets a list of members that match a predicate
        /// </summary>
        public static List<MemberInfo> GetMembers(this Type type, Func<MemberInfo, bool> predicate)
        {
            return type.GetMembers().Where(predicate).ToList();
        }

        /// <summary>
        /// Gets a list of methods that match a predicate
        /// </summary>
        public static List<MethodInfo> GetMethods(this Type type, Func<MethodInfo, bool> predicate)
        {
            return type.GetMethods().Where(predicate).ToList();
        }

        /// <summary>
        /// Gets a list of properties that match a predicate
        /// </summary>
        public static List<PropertyInfo> GetProperties(this Type type, Func<PropertyInfo, bool> predicate)
        {
            return type.GetProperties().Where(predicate).ToList();
        }

        /// <summary>
        /// Gets a list of methods that match a predicate
        /// </summary>
        public static List<MethodInfo> GetMethods(this Assembly assembly, Func<MethodInfo, bool> predicate)
        {
            var toReturn = new List<MethodInfo>();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                toReturn.AddRange(type.GetMethods(predicate));
            }

            return toReturn;
        }

        /// <summary>
        /// Determines if a member info has the given attribute
        /// </summary>
        public static bool HasAttribute<T>(this MemberInfo info) where T : Attribute
        {
            return info.DeclaringType.Get<T>(info.Name) != null;
        }

        /// <summary>
        /// Executes a method with a void return type
        /// </summary>
        public static void Execute(this Type type, object instance, string methodName, params object[] args) =>
            type.MasterExecute<object>(instance, methodName, args);

        /// <summary>
        /// Executes a method with an expected return type
        /// </summary>
        public static T Execute<T>(this Type type, object instance, string methodName, params object[] args) =>
            type.MasterExecute<T>(instance, methodName, args);

        /// <summary>
        /// The worker method that handles all of the execution of a method
        /// </summary>
        private static T MasterExecute<T>(this Type type, object instance, string methodName, object[] args)
        {
            var methods = type.GetMethods().Where(m => m.Name.Contains("Parse")).Select(m => m.Name).ToList();
            var s = type.GetMethods().ToList();
            foreach (var method in type.GetMethods())
            {
                if (method.Name.ToLower() == methodName.ToLower() && args.Length == method.GetParameters().Count())
                {
                    var data = type.Get<ConfirmRunAttribute>(methodName);
                    data?.Confirm();

                    try { return (T)method.Invoke(instance, args); }
                    catch (Exception ex) { throw ex; }
                }
            }

            throw new MemberDoesNotExistException();
        }
        // ---------------------------------------------------------------------------------------------
        #endregion



        #region Classes
        /// <summary>
        /// Gets all children classes of a given type of a given assembly
        /// </summary>
        public static List<Type> GetChildClasses(this Type type, Assembly assembly)
        {
            return assembly.GetTypes().ToList().FindAll(t => t.BaseType == type);
        }

        /// <summary>
        /// Gets all children classes of a given type of the types original assembly
        /// </summary>
        public static List<Type> GetChildClasses(this Type type)
        {
            return type.GetChildClasses(type.Assembly);
        }
        #endregion
    }
}