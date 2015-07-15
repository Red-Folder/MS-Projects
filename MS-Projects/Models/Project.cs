using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedFolder.Microservices.Projects.Models
{
    public class Project
    {
        public string Name { get; private set; }

        public Project(string name)
        {
            Name = name;
        }
    }
}