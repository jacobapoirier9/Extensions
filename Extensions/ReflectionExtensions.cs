using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Extensions
{
    public static class ReflectionExtensions
    {
        #region Attributes
        /// <include file="Summary.xml" path="OverLoads/GetAttribute/*"></include>
        public static T Get<T>(this object instance) where T : Attribute =>
            instance.GetType().Get<T>();

        /// <include file="Summary.xml" path="OverLoads/GetAttribute/*"></include>
        public static T Get<T>(this Type type) where T : Attribute
        {
            foreach (var data in type.GetCustomAttributes())
            {
                try { return (T)(object)(data); }
                catch { }
            }

            return default;
        }

        /// <include file="Summary.xml" path="OverLoads/GetAttribute/*"></include>
        public static T Get<T>(this object instance, string member) where T : Attribute =>
            instance.GetType().Get<T>(member);

        /// <include file="Summary.xml" path="OverLoads/GetAttribute/*"></include>
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
        public static List<MethodInfo> GetCustomMethods(this object instance, Func<MethodInfo, bool> predicate) =>
            instance.GetType().GetCustomMethods().Where(predicate).ToList();

        public static List<MethodInfo> GetCustomMethods(this object instance) =>
            instance.GetType().GetCustomMethods().ToList();

        public static List<MethodInfo> GetCustomMethods(this Type type, Func<MethodInfo, bool> predicate) =>
            type.GetCustomMethods().Where(predicate).ToList();
        
        public static List<MethodInfo> GetCustomMethods(this Type type) =>
            type.GetMethods().Where(mi => mi.DeclaringType == type).ToList();






        /// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        public static void Execute(this object instance, string methodName, params object[] args) =>
            instance.GetType().Execute<object>(methodName, args, instance: instance);

        /// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        public static T Execute<T>(this object instance, string methodName, params object[] args) =>
            instance.GetType().Execute<T>(methodName, args);

        /// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        public static void Execute(this Type type, string methodName, params object[] args) =>
            type.Execute<object>(methodName, args);

        /// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        public static T Execute<T>(this Type type, string methodName, object[] args, object instance = null)
        {
            foreach (var method in type.GetMethods())
            {
                if (method.Name.ToLower() == methodName.ToLower() && args.Length == method.GetParameters().Count())
                {
                    var data = type.Get<ConfirmRunAttribute>(methodName);
                    //if (data != null)
                    //{
                    //    data.Confirm();
                    //}

                    data?.Confirm();

                    try { return (T)method.Invoke(instance, args); }
                    catch (Exception ex) { throw ex; }
                }
            }
            throw new MemberDoesNotExistException();
        }


        ///// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        //public static void Execute(this object instance, string methodName, params object[] args) =>
        //    instance.Execute<object>(methodName, args);

        ///// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        //public static void Execute(this object instance, ExecuteMethodRequest request) =>
        //    instance.Execute<object>(request);

        ///// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        //public static T Execute<T>(this object instance, string methodName, params object[] args) =>
        //    instance.Execute<T>(new ExecuteMethodRequest() { MethodName = methodName, Args = args });

        ///// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        //public static T Execute<T>(this object instance, ExecuteMethodRequest request) =>
        //   instance.GetType().Execute<T>(request, instance: instance);

        ///// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        //public static void Execute(this Type type, string methodName, params object[] args) =>
        //    type.Execute<object>(methodName, args);

        ///// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        //public static void Execute(this Type type, ExecuteMethodRequest request) =>
        //    type.Execute<object>(request);

        ///// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        //public static T Execute<T>(this Type type, string methodName, params object[] args) =>
        //    type.Execute<T>(new ExecuteMethodRequest() { MethodName = methodName, Args = args });

        ///// <include file="Summary.xml" path="OverLoads/Execute/*"></include>
        //public static T Execute<T>(this Type type, ExecuteMethodRequest request, object instance = null)
        //{
        //    foreach (var method in type.GetMethods())
        //    {
        //        if (method.Name.ToLower() == request.MethodName.ToLower())
        //        {
        //            var data = type.Get<ConfirmRunAttribute>(request.MethodName);
        //            if (data != null)
        //            {
        //                data.Confirm();
        //            }

        //            try { return (T)method.Invoke(instance, request.Args); }
        //            catch (Exception ex) { throw ex; }
        //        }
        //    }
        //    throw new MemberDoesNotExistException();
        //}
        #endregion



        #region Parameters
        public static List<ParameterInfo> GetParameters(this object instnace, string methodName) =>
            instnace.GetType().GetParameters(methodName);
        
        public static List<ParameterInfo> GetParameters(this Type type, string methodName) =>
            type.GetCustomMethods().Find(m => m.Name == methodName).GetParameters().ToList();
        #endregion



        #region Classes
        public static List<Type> GetChildClasses(this Type type) =>
            type.Assembly.GetTypes().ToList().FindAll(t => t.BaseType == type);
        #endregion
    }
}