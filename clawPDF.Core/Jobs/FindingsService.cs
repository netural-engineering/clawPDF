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

namespace clawSoft.clawPDF.Core.Jobs
{
    class FindingsService
    {

        public static TokenResponseDto LogIn(TokenRequestDto tokenRequest)
        {
            WebClient myWebClient = new WebClient();
            NameValueCollection myNameValueCollection = new NameValueCollection();

            myNameValueCollection.Add("client_id", tokenRequest.ClientId);
            myNameValueCollection.Add("client_secret", tokenRequest.ClientSecret);
            myNameValueCollection.Add("grant_type", tokenRequest.GrantType);
            myNameValueCollection.Add("username", tokenRequest.Username);
            myNameValueCollection.Add("password", tokenRequest.Password);

            byte[] responseArray = myWebClient.UploadValues("https://dev-identity.vivellio.app/auth/realms/Vivellio/protocol/openid-connect/token", myNameValueCollection);
            string responseStr = Encoding.ASCII.GetString(responseArray);

            TokenResponseDto tokenResponse = JsonConvert.DeserializeObject<TokenResponseDto>(responseStr);

            return tokenResponse;
        }

        public static void UploadDoctorFinding(string token, string filePath)
        {
            string boundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + boundary;
            byte[] multiformData = BuildMultiformData("file", filePath, boundary);

            WebClient myWebClient = new WebClient();
            myWebClient.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
            myWebClient.Headers[HttpRequestHeader.ContentType] = contentType;

            byte[] responseArray = myWebClient.UploadData("https://dev-app-gate.vivellio.app/printer-driver/upload-finding", "POST", multiformData);
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

        private static Dictionary<string, string> toDictionary(TokenRequestDto tokenRequest)
        {
            var json = JsonConvert.SerializeObject(tokenRequest);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
