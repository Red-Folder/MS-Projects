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

        public string Description { get; private set; }

        public Uri LogoUrl { get; private set; }

        public List<string> SHAs { get; private set; }

        public List<SubModule> SubModules { get; private set; }

        public Repository(int id, string name, string description, Uri logoUrl, List<string> shas, List<SubModule> submodules)
        {
            Id = id;
            Name = name;
            Description = description;
            LogoUrl = logoUrl;
            SHAs = shas;
            SubModules = submodules;
        }
    }
}
