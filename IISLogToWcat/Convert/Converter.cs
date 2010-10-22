using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IISLogToWcat.Convert
{
    public class Converter
    {
        public IEnumerable<WebTestRequest> GetRequestEnumerator(string pathToLogFile, string server)
        {
            if (pathToLogFile == null) throw new ArgumentNullException("pathToLogFile");

            IISLogReader reader = new IISLogReader(pathToLogFile, server);

            return reader.GetRequests();
          
        }
    }
}