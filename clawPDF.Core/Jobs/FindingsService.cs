using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;

namespace clawSoft.clawPDF.Core.Jobs
{
    class FindingsService
    {
        public static TokenResponseDto LogIn(TokenRequestDto tokenRequest)
        {
            HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            client.BaseAddress = new Uri("https://dev-identity.vivellio.app/auth/realms/Vivellio/protocol/openid-connect/token");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var valuesDictionary = ToDictionary(tokenRequest);
            var requestBody = new FormUrlEncodedContent(valuesDictionary);

            TokenResponseDto tokenResponse = null;

            try
            {

                var task = client.PostAsync("", requestBody).ContinueWith(async t =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        var response = t.Result;
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            tokenResponse = await t.Result.Content.ReadAsAsync<TokenResponseDto>();
                        }
                    }

                });

                task.Wait();
                client.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tokenResponse;
        }

        public static bool UploadDoctorFinding(string token, string filePath)
        {

            string srcFilename = Path.GetFileName(filePath); ;
            string destFileName = srcFilename;

            var uploaded = false;
            try
            {
                HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
                client.BaseAddress = new Uri("https://dev-app-gate.vivellio.app/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var fileStream = File.Open(filePath, FileMode.Open);
                var fileInfo = new FileInfo(srcFilename);

                var content = new MultipartFormDataContent();
                content.Headers.Add("filePath", filePath);
                content.Add(new StreamContent(fileStream), "\"file\"", string.Format("\"{0}\"", destFileName + fileInfo.Extension));

                var task = client.PostAsync("printer-driver/upload-finding", content).ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        var response = t.Result;
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            uploaded = true;
                        }
                    }

                    fileStream.Dispose();
                });

                task.Wait();
                client.Dispose();
            }
            catch (Exception ex)
            {
                uploaded = false;
                throw ex;
            }

            return uploaded;
        }

        private static Dictionary<string, string> ToDictionary(TokenRequestDto tokenRequest)
        {
            var json = JsonConvert.SerializeObject(tokenRequest);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
