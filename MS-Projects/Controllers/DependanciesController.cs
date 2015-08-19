using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RedFolder.Microservices.Projects.Models;
using RedFolder.Microservices.Proxies.GitHub;
using System.Threading.Tasks;

using System.Web.Http.Cors;
using System.Runtime.Caching;

namespace RedFolder.Microservices.Projects.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DependanciesController : ApiController
    {
        public async Task<DependancyGraph> GetAllDependancies()
        {
            ObjectCache cache = MemoryCache.Default;

            var graph = cache.Get("DependancyGraph") as DependancyGraph;
            if (graph != null)
            {
                return graph;
            }

            var client = new Client();
            var repositories = await client.GetProjects(GitHubCredentials.User, GitHubCredentials.Key);// .Result;

            var nodes = new List<Project>();
            var links = new List<Link>();

            nodes.AddRange(from r in repositories
                           select new Project(r.Name, "TODO", "https://github.com/Red-Folder/MS-Projects/ProjectLogo.png"));

            foreach (var repository in repositories.Where(x => x.SubModules.Length > 0))
            {
                int source = nodes.FindIndex(x => x.Name == repository.Name);

                foreach (var subModule in repository.SubModules)
                {
                    int target = nodes.FindIndex(x => x.Name == subModule.Name);
                    links.Add(new Link(source, target, "green", new List<string>()));
                }
            }

            graph = new DependancyGraph(nodes.ToArray(), links.ToArray());
            CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) };
            cache.Add("DependancyGraph", graph, policy);

            return graph;
        }
    }
}
