using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Autoprefixer
{
    [DataContract]
	public sealed class Options
	{
        [DataMember(Name = "cascade")]
        public bool Cascade { get; set; }
	}
}