using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace clawSoft.clawPDF.Core.Jobs
{
    public class PatientDto
    {
        [JsonProperty("profileId")]
        public string ProfileId { get; set; }

        [JsonProperty("socialSecurityNumber")]
        public string SocialSecurityNumber { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

    }
}
