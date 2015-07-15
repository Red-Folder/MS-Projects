using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedFolder.Microservices.Projects.Models
{
    public class DependancyGraph
    {
        public Project[] Nodes { get; private set;}
        public Link[] Links { get; private set; }

        public DependancyGraph(Project[] nodes, Link[] links)
        {
            Nodes = nodes;
            Links = links;
        }
    }
}