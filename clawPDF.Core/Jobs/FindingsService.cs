using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using Microsoft.Win32;

namespace clawSoft.clawPDF.Core.Jobs
{
    class FindingsService
    {
        public static MatchingPatientsDto UploadDoctorFinding(string filePath)
        {
            string boundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + boundary;
            byte[] multiformData = BuildMultiformData("file", filePath, boundary);

            WebClient myWebClient = new WebClient();
            myWebClient.Headers[HttpRequestHeader.ContentType] = contentType;
            myWebClient.Headers["Driver-License-Key"] = GetKey();

            byte[] responseArray = myWebClient.UploadData("https://qa-app-gate.vivellio.app/printer-driver/upload-finding", "POST", multiformData);
            string responseStr = Encoding.ASCII.GetString(responseArray);

            return JsonConvert.DeserializeObject<MatchingPatientsDto>(responseStr);
        }

        private static byte[] BuildMultiformData(string propertyName, string filePath, string boundary)
        {
            Encoding encoding = Encoding.UTF8;

            string fileName = Path.GetFileName(filePath);
            byte[] fileBytes = File.ReadAllBytes(filePath);

            Stream formDataStream = new MemoryStream();

            string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        propertyName,
                        fileName,
                        "application/pdf");

            formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

            // Write the file data directly to the Stream, rather than serializing it to a string.  
            formDataStream.Write(fileBytes, 0, fileBytes.Length);

            // Add the end of the request.  Start with a newline  
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]  
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        private static Dictionary<string, string> toDictionary(MatchingPatientsDto tokenRequest)
        {
            var json = JsonConvert.SerializeObject(tokenRequest);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        private static string GetKey()
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Vivellio\LICENSE");
            return (string) registryKey.GetValue("KEY");
        }
    }
}
