using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.PrinterDriver.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clawSoft.clawPDF.Core.PrinterDriver
{
    public interface IPrinterDriverService
    {
        MatchingResultDto UploadDoctorFinding(string filePath, Metadata metadata);
        InstallationNotificationDto ValidateLicense();
    }
}
