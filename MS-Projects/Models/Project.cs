using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RedFolder.Microservices.Projects.Models
{
    [DataContract(Name = "project")]
    public class Project
    {
        [DataMember(Name = "name")]
        public string Name { get; private set; }
        [DataMember(Name = "description")]
        public string Description { get; private set; }
        [DataMember(Name = "logo_url")]
        public string LogoURL { get; private set; }

        public Project(string name, string description, string logourl)
        {
            Name = name;
            Description = description;
            LogoURL = logourl;
        }   
    }
}