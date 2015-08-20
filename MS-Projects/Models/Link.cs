using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        [JsonConverter(typeof(StringEnumConverter))]
        public RAG Rag { get; private set; }
        [DataMember(Name = "rag_message")]
        public string Rag_Message
        {
            get
            {
                if (Rag == RAG.red)
                {
                    return "Missing commit";
                }
                else
                {
                    if (Rag == RAG.amber)
                    {
                        return String.Format("{0} commit(s) behind master", SHAs_Behind.Count);
                    }
                    else
                    {
                        return "All good";
                    }
                }
            }
        }
        private IList<string> SHAs_Behind { get; set; }

        public Link(int source, int target, RAG rag, List<string> shasBehind)
        {
            Source = source;
            Target = target;
            Rag = rag;
            SHAs_Behind = shasBehind.AsReadOnly();
        }

        public enum RAG
        {
            red,
            amber,
            green
        }

    }

}