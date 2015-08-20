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
                           select new Project(r.Name, r.Description, r.LogoUrl.ToString()));

            foreach (var repository in repositories.Where(x => x.SubModules.Count > 0))
            {
                int source = nodes.FindIndex(x => x.Name == repository.Name);

                foreach (var subModule in repository.SubModules)
                {
                    int target = nodes.FindIndex(x => x.Name == subModule.Name);

                    var subModuleRepo = repositories.Where(x => x.Name == subModule.Name).FirstOrDefault();

                    // Have we found the subModule - if not then skip - likely to be a remote submodule
                    if (subModuleRepo != null)
                    {
                        // TODO - Convert to an enum
                        Link.RAG rag = Link.RAG.red;
                        var shasBehind = new List<string>();

                        // Does the SHA exist at all (likely to be due to changes in the submodule folder not being checked in
                        if (subModuleRepo.SHAs.Where(x => x == subModule.SHA).Count() > 0)
                        { 
                            // Is this the latest SHA?
                            if (subModule.SHA.Equals(subModuleRepo.SHAs[0]))
                            {
                                rag = Link.RAG.green;
                            }
                            else
                            {
                                // How many SHAs are we behind?
                                var indexNo = subModuleRepo.SHAs.FindIndex(x => x == subModule.SHA);

                                shasBehind.AddRange(from sha in subModuleRepo.SHAs.GetRange(0, indexNo)
                                                    select sha);

                                rag = Link.RAG.amber;
                            }
                        }

                        links.Add(new Link(source, target, rag, shasBehind));
                    }
                }
            }

            graph = new DependancyGraph(nodes.ToArray(), links.ToArray());
            CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) };
            cache.Add("DependancyGraph", graph, policy);

            return graph;
        }

    }
}
