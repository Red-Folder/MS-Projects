using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RedFolder.Microservices.Projects.Models;
using RedFolder.Microservices.Proxies.GitHub;
using System.Threading.Tasks;

namespace RedFolder.Microservices.Projects.Controllers
{
    public class DependanciesController : ApiController
    {
        //DependancyGraph graph = new DependancyGraph
        //{
        //    Nodes = new Project[] { 
        //        new Project {Name = "Project 1"},
        //        new Project {Name = "Project 2"},
        //        new Project {Name = "Project 3"},
        //        new Project {Name = "Project 4"},
        //        new Project {Name = "Project 5"}
        //    },

        //    Links = new Link[] { 
        //        new Link { Source = 0, Target = 1 },
        //        new Link { Source = 0, Target = 2 },
        //        new Link { Source = 1, Target = 3 },
        //        new Link { Source = 2, Target = 3 },
        //        new Link { Source = 3, Target = 4 },
        //        new Link { Source = 3, Target = 5 }

        //    }
        //};

        public async Task<DependancyGraph> GetAllDependancies()
        {

            var repositories = await Client.GetProjects();// .Result;

            var nodes = new List<Project>();
            var links = new List<Link>();

            nodes.AddRange(from r in repositories
                           select new Project(r.Name));

            foreach (var repository in repositories.Where(x => x.SubModules.Length > 0))
            {
                int source = nodes.FindIndex(x => x.Name == repository.Name);

                foreach (var subModule in repository.SubModules)
                {
                    int target = nodes.FindIndex(x => x.Name == subModule.Name);
                    links.Add(new Link(source, target));
                }
            }

            return new DependancyGraph(nodes.ToArray(), links.ToArray());
        }
    }
}
