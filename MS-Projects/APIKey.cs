using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;

namespace RedFolder.Microservices.Projects
{
    public class APIKey
    {
        public static string Key
        {
            get
            {
                return File.ReadAllText(@"c:\tmp\GitHubKey.txt");
            }
        }
    }
}