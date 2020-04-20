using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bleff.CustomExtensions
{
    public static class WebExtensions
    {
        public static object Get(this System.Web.HttpApplicationStateBase application, Enum en)
        {
            return application[en.GetType().Name + "_" + en.ToString()];
        }

        public static T Get<T>(this System.Web.HttpApplicationStateBase application, Enum en)
        {
            return (T)application[en.GetType().Name + "_" + en.ToString()];
        }

        public static void Set(this System.Web.HttpApplicationStateBase application, Enum en, object value)
        {
            application[en.GetType().Name + "_" + en.ToString()] = value;
        }

        public static object Get(this System.Web.HttpApplicationState application, Enum en)
        {
            return application[en.GetType().Name + "_" + en.ToString()];
        }

        public static T Get<T>(this System.Web.HttpApplicationState application, Enum en)
        {
            return (T)application[en.GetType().Name + "_" + en.ToString()];
        }

        public static void Set(this System.Web.HttpApplicationState application, Enum en, object value)
        {
            application[en.GetType().Name + "_" + en.ToString()] = value;
        }

        public static object Get(this System.Web.SessionState.HttpSessionState session, Enum en)
        {
            return session[en.GetType().Name + "_" + en.ToString()];
        }

        public static T Get<T>(this System.Web.SessionState.HttpSessionState session, Enum en)
        {
            return (T)session[en.GetType().Name + "_" + en.ToString()];
        }

        public static object Get(this System.Web.HttpSessionStateBase session, Enum en)
        {
            return session[en.GetType().Name + "_" + en.ToString()];
        }

        public static T Get<T>(this System.Web.HttpSessionStateBase session, Enum en)
        {
            return (T)session[en.GetType().Name + "_" + en.ToString()];
        }

        public static void Set(this System.Web.HttpSessionStateBase session, Enum en, object value)
        {
            session[en.GetType().Name + "_" + en.ToString()] = value;
        }

        public static void Set(this System.Web.SessionState.HttpSessionState session, Enum en, object value)
        {
            session[en.GetType().Name + "_" + en.ToString()] = value;
        }
    }
}