﻿using System.Net.Http;
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
    class PrinterDriverService
    {
        public static MatchingPatientsDto UploadDoctorFinding(string filePath, Metadata metadata)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("file", filePath);
            parameters.Add("title", metadata.Title);
            parameters.Add("bodyPart", metadata.BodyPart);

            string boundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + boundary;
            byte[] multiformData = BuildMultiformData(parameters, boundary);

            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.ContentType] = contentType;
            webClient.Headers["Driver-License-Key"] = GetKey();

            try
            {
                byte[] responseArray = webClient.UploadData("https://qa-app-gate.vivellio.app/printer-driver/upload-finding", "POST", multiformData);
                string responseStr = Encoding.ASCII.GetString(responseArray);
                return JsonConvert.DeserializeObject<MatchingPatientsDto>(responseStr);
            }
            catch (WebException e)
            {
                //if (e.Status.Equals(401))
                return null;

            }            
        }

        private static byte[] BuildMultiformData(Dictionary<string, string> postParameters, string boundary)
        {
            Encoding encoding = Encoding.UTF8;
            Stream formDataStream = new MemoryStream();
            bool isNotFirstParam = false;

            foreach (var param in postParameters)
            {
                if (isNotFirstParam)
                {
                    // add parameter as a separator
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));
                }

                isNotFirstParam = true;

                if (param.Key is "file")
                {
                    string fileName = Path.GetFileName(param.Key);
                    byte[] fileBytes = File.ReadAllBytes(param.Value);

                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        param.Value,
                        "application/pdf");

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));
                    formDataStream.Write(fileBytes, 0, fileBytes.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }

            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

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
