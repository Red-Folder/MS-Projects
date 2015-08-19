using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Microservices.Proxies.GitHub.Models
{
    public class APIMetric
    {
        public string APICall { get; private set; }
        public int Limit { get; private set; }
        public int Remaining { get; private set; }

        public APIMetric(string apiCall, int limit, int remaining)
        {
            APICall = apiCall;
            Limit = limit;
            Remaining = remaining;
        }
    }
}
