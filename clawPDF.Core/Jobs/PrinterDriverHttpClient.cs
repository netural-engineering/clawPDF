using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

//https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

namespace clawSoft.clawPDF.Core.Jobs
{
   
    public class PrinterDriverHttpClient
    {
        static HttpClient client;

        public PrinterDriverHttpClient() {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://dev-app-gate.vivellio.app/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("multipart/form-data"));
        }

        static async Task<HttpStatusCode> CreateProductAsync(PdfFileDataDto fileData)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "printer-driver/upload-finding", fileData);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.StatusCode;
        }


        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://dev-app-gate.vivellio.app/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                PdfFileDataDto fileData = new PdfFileDataDto
                {
                    File = "Gizmo"
                };

                var url = await CreateProductAsync(fileData);
                Console.WriteLine($"Created at {url}");


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}