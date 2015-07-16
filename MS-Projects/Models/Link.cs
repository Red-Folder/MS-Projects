using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedFolder.Microservices.Projects.Models
{
    public class Link
    {
        public int Source { get; private set; }
        public int Target { get; private set; }

        public Link(int source, int target)
        {
            Source = source;
            Target = target;
        }
    }
}