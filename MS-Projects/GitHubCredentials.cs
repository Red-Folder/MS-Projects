using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using Microsoft.WindowsAzure;

using System.Configuration;

namespace RedFolder.Microservices.Projects
{
    public class GitHubCredentials
    {
        private const string _FILE = "FILE";

        public static string User
        {
            get
            {
                return ConfigurationManager.AppSettings["GitHubUser"];
            }
        }

        public static string Key
        {
            get
            {
                var apikey = CloudConfigurationManager.GetSetting("GitHubAPIKey");

                if (_FILE.Equals(apikey))
                    return File.ReadAllText(@"c:\tmp\GitHubKey.txt");
                else
                    return apikey;
            }
        }
    }
}