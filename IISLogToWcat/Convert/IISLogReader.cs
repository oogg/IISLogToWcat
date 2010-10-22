using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSUtil;

namespace IISLogToWcat.Convert
{
    public class IISLogReader
    {
        private string m_iisLogPath;

        public IISLogReader(string iisLogPath) : this(iisLogPath, null)
        {
        }

        public IISLogReader(string iisLogPath, string server)
        {
            m_iisLogPath = iisLogPath;
        }

        public IEnumerable<WebTestRequest> GetRequests()
        {
            LogQueryClassClass logQuery = new LogQueryClassClass();
            COMIISW3CInputContextClassClass iisInputFormat = new COMIISW3CInputContextClassClass();

            string query = @"SELECT s-port, cs-method, cs-uri-stem, cs-uri-query FROM " + m_iisLogPath;

            ILogRecordset recordSet = logQuery.Execute(query, iisInputFormat);
            while (!recordSet.atEnd())
            {
                ILogRecord record = recordSet.getRecord();
                if (record.getValueEx("cs-method").ToString() == "GET")
                {
                    string path = record.getValueEx("cs-uri-stem").ToString();
                    string querystring = record.getValueEx("cs-uri-query").ToString();

                    var urlBuilder = new StringBuilder();
                    urlBuilder.Append(path);
                    if (!String.IsNullOrEmpty(querystring))
                    {
                        urlBuilder.Append("?");
                        urlBuilder.Append(querystring);
                    }

                    var request = new WebTestRequest(urlBuilder.ToString());
                    yield return request;
                }

                recordSet.moveNext();
            }

            recordSet.close();
        }
    }
}