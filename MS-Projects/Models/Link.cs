using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RedFolder.Microservices.Projects.Models
{
    [DataContract(Name = "link")]
    public class Link
    {
        [DataMember(Name = "source")]
        public int Source { get; private set; }
        [DataMember(Name = "target")]
        public int Target { get; private set; }
        [DataMember(Name = "rag")]
        public string RAG { get; private set; }
        [DataMember(Name = "shas_behind")]
        public IList<string> SHAs_Behind { get; private set; }

        public Link(int source, int target, string rag, List<string> shasBehind)
        {
            Source = source;
            Target = target;
            RAG = rag;
            SHAs_Behind = shasBehind.AsReadOnly();
        }
    }
}