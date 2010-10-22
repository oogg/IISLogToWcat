using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IISLogToWcat.Convert;

namespace IISLogToWcat
{
    class Program
    {
        static void Main(string[] args)
        {

            //args
            string serverThatWcatWillRunAgainst = "api";
            string pathToLog = @"C:\temp\u_ex101019.log";
            string pathToRequestFile = @"C:\temp\requests.ubr";

            CreateRequests(pathToLog, serverThatWcatWillRunAgainst, pathToRequestFile);
        }

        private static void CreateRequests(string log, string server, string pathToRequestFile)
        {
            var converter = new Converter();
            var requests = converter.GetRequestEnumerator(log, server);

            var requestsBuilder = new StringBuilder();

            foreach (var request in requests)
            {
                requestsBuilder.Append("request");
                requestsBuilder.Append(Environment.NewLine + "{");
                requestsBuilder.Append(Environment.NewLine + @"url =""" + request.Url + @""";");
                requestsBuilder.Append(Environment.NewLine + @"statuscode=200;");
                requestsBuilder.Append(Environment.NewLine);
                requestsBuilder.Append("}");
                requestsBuilder.Append(Environment.NewLine + Environment.NewLine);
            }

            File.WriteAllText(pathToRequestFile, requestsBuilder.ToString());
        }
    }
}