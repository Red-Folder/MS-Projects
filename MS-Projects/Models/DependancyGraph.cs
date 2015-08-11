using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace RedFolder.Microservices.Projects.Models
{
    [DataContract(Name = "graph")]
    public class DependancyGraph
    {
        [DataMember(Name = "nodes")]
        public Project[] Nodes { get; private set;}
        [DataMember(Name = "links")]
        public Link[] Links { get; private set; }

        public DependancyGraph(Project[] nodes, Link[] links)
        {
            Nodes = nodes;
            Links = links;
        }
    }
}