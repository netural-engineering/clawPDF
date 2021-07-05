using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace clawSoft.clawPDF.Core.Jobs
{
    public enum ResponseCodeDto
    {
        NO_MATCH = 0,
        SINGLE_MATCH = 1,
        MULTIPLE_MATCHES = 2
    }

    public class MatchingPatientsDto
    {
        [JsonProperty("responseCode")]
        public ResponseCodeDto ResponseCode { get; set; }

        [JsonProperty("patients")]
        public List<PatientDto> Patients { get; set; }
    }
}
