using Newtonsoft.Json;

namespace clawSoft.clawPDF.PrinterDriver.Domain
{
    public enum ResponseCodeDto
    {
        INVALID_KEY = -1,
        NO_MATCH = 0,
        SINGLE_MATCH = 1,
        MULTIPLE_MATCHES = 2
    }

    public class MatchingResultDto
    {
        [JsonProperty("responseCode")]
        public ResponseCodeDto ResponseCode { get; set; }
    }
}
