using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;

namespace StarNet.Core
{
    public class UrlRemapUtil
    {
        public static void ReplaceJQueryToNewVersionInUrlPath(HttpContext httpContext)
        {
            var jqueryAndMoreUrls = new string[]
            {
                "~/Resources/Scripts/jQuery.js",
                "~/Resources/Scripts/GizmoxAddedToJQuery.js"
            };

            SetNewFilesInUrlPath(HttpContext.Current, "Resources.Includes.js.wgx", jqueryAndMoreUrls);
        }
        public static HttpContext SetNewFilesInUrlPath(HttpContext httpContext, string endWithPath, string[] newFileNames)
        {
            if (httpContext.Request.AppRelativeCurrentExecutionFilePath.EndsWith(endWithPath, StringComparison.OrdinalIgnoreCase))
            {

                string responseStrResult = "";

                foreach (string fileName in newFileNames)
                {
                    string physicalPath = httpContext.Server.MapPath(fileName);

                    string fileStrContent = System.IO.File.ReadAllText(physicalPath);

                    responseStrResult += "\n\r" + fileStrContent;
                }

                httpContext.Response.ContentType = "application/javascript";
                httpContext.Response.Write(responseStrResult);
                httpContext.Response.End();
            }
            return httpContext;
        }
    }
}