//using Todo.Common.Models.API.Request;
using Todo.Common.Models.CMS;
//using Todo.Common.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Serilog;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Web;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Strings;
using static Lucene.Net.Analysis.Sinks.TeeSinkTokenFilter;
using static Umbraco.Cms.Core.Constants.HttpContext;
using System;


namespace Todo.Common.Helpers
{
    public static class Common
    {
        private static ILogger _logger = Log.Logger;

        public static void Initialize()
        {
            _logger.Information("Initialize");
        }

        public static T MapTo<T>(this object sourceObject)
        {
            using (new FunctionTracer())
            {
                var child = (T)Activator.CreateInstance(typeof(T));
                var fromProperties = sourceObject.GetType().GetProperties();
                var toProperties = child.GetType().GetProperties();

                foreach (var fromProperty in fromProperties)
                {
                    foreach (var toProperty in toProperties)
                    {
                        if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                        {
                            if (!toProperty.CanWrite) break;
                            toProperty.SetValue(child, fromProperty.GetValue(sourceObject));
                            break;
                        }
                    }
                }
                return child;
            }
        }

        public static (string parentalias, string alias, string childalias) GetNodeInfo(IPublishedContent content)
        {
            string parentalias = "";
            string alias = "";
            string childalias = "";

            if (content != null)
            {
                parentalias = content?.Parent?.ContentType.Alias ?? string.Empty;
                alias = content.ContentType.Alias;
                childalias = content?.Children.FirstOrDefault()?.ContentType.Alias ?? string.Empty;
            }

            return (parentalias, alias, childalias);
        }

        public static (string parentalias, string alias, string childalias) GetNodeInfo(string alias)
        {
            string parentalias = "";
            string childalias = "";

            return (parentalias, alias, childalias);
        }



        //public static string formatDate(DateTime fromDate, DateTime toDate, string culture)
        //{
        //    string[] customMonth = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //    string[] customMonthAR = { "يناير", "فبراير", "مارس", "أبريل", "مايو", "يونيو", "يوليو", "أغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر" };
        //    string[] customDay = { "Mon", "Tus", "Wed", "Thu", "Fri", "Sat", "Sun" };
        //    string[] customDayAR = { "Mon", "Tus", "Wed", "Thu", "Fri", "Sat", "Sun" };

        //    //var fromDate = Model.FromDate;
        //    //var toDate = Model.ToDate;
        //    //var startingMonth = Model.FromDate.ToString("MMM", CultureInfo.CreateSpecificCulture(culture));
        //    string[] monthArr = (culture == "ar") ? customMonthAR : customMonth;
        //    string startingNumaricMonth = fromDate.ToString("MM");
        //    string endingNumaricMonth = toDate.ToString("MM");
        //    string startingNumaricYear = fromDate.ToString("yyyy");
        //    string endingNumaricYear = toDate.ToString("yyyy");
        //    string finalDate = "";
        //    if (toDate == DateTime.MinValue || fromDate.ToString("dd MM yyyy") == toDate.ToString("dd MM yyyy"))
        //    {
        //        finalDate = fromDate.ToString("ddd", CultureInfo.CreateSpecificCulture(culture)) + ", " + fromDate.ToString("dd") + " " + monthArr[int.Parse(fromDate.ToString("MM")) - 1] + " " + fromDate.ToString("yyyy");
        //    }
        //    else
        //    {
        //        if (startingNumaricMonth == endingNumaricMonth)
        //        {
        //            if (startingNumaricYear == endingNumaricYear)
        //            {
        //                finalDate = fromDate.ToString("ddd", CultureInfo.CreateSpecificCulture(culture)) + ", " + fromDate.ToString("dd") + " - " + toDate.ToString("ddd", CultureInfo.CreateSpecificCulture(culture)) + ", " + toDate.ToString("dd") + " " + monthArr[int.Parse(toDate.ToString("MM")) - 1] + " " + toDate.ToString("yyyy");
        //            }
        //            else
        //            {
        //                finalDate = fromDate.ToString("ddd", CultureInfo.CreateSpecificCulture(culture)) + ", " + fromDate.ToString("dd") + " " + monthArr[int.Parse(fromDate.ToString("MM")) - 1] + " " + fromDate.ToString("yyyy") + " - " + toDate.ToString("ddd", CultureInfo.CreateSpecificCulture(culture)) + ", " + toDate.ToString("dd") + " " + monthArr[int.Parse(toDate.ToString("MM")) - 1] + " " + toDate.ToString("yyyy");
        //            }
        //        }
        //        else
        //        {
        //            if (startingNumaricYear == endingNumaricYear)
        //            {
        //                finalDate = fromDate.ToString("ddd", CultureInfo.CreateSpecificCulture(culture)) + ", " + fromDate.ToString("dd") + " " + monthArr[int.Parse(fromDate.ToString("MM")) - 1] + " - " + toDate.ToString("ddd", CultureInfo.CreateSpecificCulture(culture)) + ", " + toDate.ToString("dd") + " " + monthArr[int.Parse(toDate.ToString("MM")) - 1] + " " + toDate.ToString("yyyy");
        //            }
        //            else
        //            {
        //                finalDate = fromDate.ToString("ddd", CultureInfo.CreateSpecificCulture(culture)) + ", " + fromDate.ToString("dd") + " " + monthArr[int.Parse(fromDate.ToString("MM")) - 1] + " " + fromDate.ToString("yyyy") + " - " + toDate.ToString("ddd", CultureInfo.CreateSpecificCulture(culture)) + ", " + toDate.ToString("dd") + " " + monthArr[int.Parse(toDate.ToString("MM")) - 1] + " " + toDate.ToString("yyyy");
        //            }
        //        }
        //    }

        //    return finalDate;
        //}

        public static string? GetDetailPageUrl(this IPublishedContent content)
        {
            if (content == null)
                return "";

            return content.Parent.ContentType.Alias + "/" + HttpUtility.HtmlEncode(content.Name);
        }
    }
}
