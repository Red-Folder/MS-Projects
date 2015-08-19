using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Microservices.Proxies.GitHub.Models
{
    public class SubModule
    {
        public string Name { get; private set; }
        public string SHA { get; private set; }

        public SubModule (string name, string sha)
        {
            Name = name;
            SHA = sha;
        }
    }
}
