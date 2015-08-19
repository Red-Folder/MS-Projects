using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Microservices.Proxies.GitHub.Models
{
    public class Repository
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public string[] SHAs { get; private set; }

        public SubModule[] SubModules { get; private set; }

        public Repository(int id, string name, string[] shas, SubModule[] submodules)
        {
            Id = id;
            Name = name;
            SHAs = shas;
            SubModules = submodules;
        }
    }
}
