using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IISLogToWcat.Convert
{
    public class WebTestRequest
    {
        public string Url { get; set; }
        public WebTestRequest(string url)
        {
            Url = url;
        }
    }
}