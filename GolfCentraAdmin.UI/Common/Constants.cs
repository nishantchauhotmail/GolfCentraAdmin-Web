using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GolfCentraAdmin.UI.Common
{
    /// <summary>
    /// Constant Details
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Session Name For Action Session
        /// </summary>
        public static string SessionName = "GolfCentra_SESSION";

        /// <summary>
        /// Site Details
        /// </summary>
        public static class Site
        {
            public static string SiteTitle = System.Configuration.ConfigurationManager.AppSettings["SiteTitle"];
        }

        /// <summary>
        /// URL Details
        /// </summary>
        public static class Url
        {
            public static string WebApiUrl = ConfigurationManager.AppSettings["WebApiUrl"];
            public static string WebSiteUrl = ConfigurationManager.AppSettings["WebSiteUrl"];
            public static string WebSiteUrlWithoutSlash = ConfigurationManager.AppSettings["WebSiteUrlWithoutSlash"];
        }

        /// <summary>
        /// API Access Details
        /// </summary>
        public static class ApiAccess
        {
            public static string UserName = ConfigurationManager.AppSettings["ApiUserName"];
            public static string Password = ConfigurationManager.AppSettings["ApiPassword"];
            public static string HttpTime = ConfigurationManager.AppSettings["HttpTime"];
            public static string ExtraHttpTime = ConfigurationManager.AppSettings["ExtraHttpTime"];
        }

        public static class Name
        {
            public static string AppName = ConfigurationManager.AppSettings["AppName"];
        }

    }
}