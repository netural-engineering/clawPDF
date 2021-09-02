using Newtonsoft.Json;

namespace clawSoft.clawPDF.PrinterDriver.Domain
{
    public class InstallationNotificationDto
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
    }
}
