using Octokit;
using Octokit.Internal;
using RedFolder.Microservices.Proxies.GitHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;



namespace RedFolder.Microservices.Proxies.GitHub
{
    public class Client
    {
        public async Task<Models.Repository[]> GetProjects(string user, string apiKey)
        {
            IList<RedFolder.Microservices.Proxies.GitHub.Models.Repository> result = new List<RedFolder.Microservices.Proxies.GitHub.Models.Repository>();

            var productInformation = new Octokit.ProductHeaderValue("MS-Projects-Microservice");
            var github = new GitHubClient(productInformation);
            github.Credentials = new Credentials(user, apiKey);
            
            try
            {
                var repositories = await github.Repository.GetAllForUser("Red-Folder");
                AddMetric("Repository.GetAllForUser", github.GetLastApiInfo());

                foreach (var repository in repositories.Where(x => x.Name.StartsWith("MS-")))
                {
                    var modelSHAs = new List<string>();

                    var commits = await github.Repository.Commits.GetAll(repository.Owner.Login, repository.Name);
                    AddMetric("Repository.Commits.GetAll", github.GetLastApiInfo());
                    foreach (var commit in commits)
                    {
                        modelSHAs.Add(commit.Sha);
                    }

                    var modelSubModules = new List<RedFolder.Microservices.Proxies.GitHub.Models.SubModule>();
                    string basePath = "/";
                    var contents = await github.Repository.Content.GetAllContents(repository.Owner.Login, repository.Name, basePath);
                    AddMetric("Repository.Content.GetAllContents", github.GetLastApiInfo());

                    var remoteFolder = contents.Where(x => x.Name.ToLower().Equals("remote")).Where(x => x.Type == ContentType.Dir).FirstOrDefault();
                    if (remoteFolder != null)
                    {
                        var remoteFiles = await github.Repository.Content.GetAllContents(repository.Owner.Login, repository.Name, remoteFolder.Path);
                        AddMetric("Repository.Content.GetAllContents", github.GetLastApiInfo());

                        foreach (var remoteFile in remoteFiles.Where(x => x.Type == ContentType.File))
                        {
                            var remoteFileContent = await github.Repository.Content.GetAllContents(repository.Owner.Login, repository.Name, remoteFile.Path);
                            AddMetric("Repository.Content.GetAllContents", github.GetLastApiInfo());

                            foreach (var subModule in remoteFileContent.Where(x => x.Type == ContentType.Submodule))
                            {
                                modelSubModules.Add(new Models.SubModule(subModule.Name, subModule.Sha));
                            }
                        }
                    }

                    result.Add(new Models.Repository(repository.Id, repository.Name, modelSHAs.ToArray(), modelSubModules.ToArray()));
                }
            }
            catch (RateLimitExceededException rateException)
            {
                //rateException.
            }

            return result.ToArray();
        }

        private void AddMetric(string apiCall, ApiInfo apiInfo)
        { 
            var metric = new APIMetric(apiCall, apiInfo.RateLimit.Limit, apiInfo.RateLimit.Remaining);
        }

        private void AddLog(string logType, string logMessage)
        {

        }
    }
}
