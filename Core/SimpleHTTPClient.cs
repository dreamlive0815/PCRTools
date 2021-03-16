
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace SimpleHTTPClient
{
    public class Client : IDisposable
    {

        private HttpClient client;

        public Client()
        {
            client = new HttpClient();
            setTimeoutMS(5000);
            setRequestHeader(Headers.UserAgent, UserAgents.Chrome);
        }

        public void setTimeoutMS(int timeoutMS)
        {
            client.Timeout = TimeSpan.FromMilliseconds(timeoutMS);
        }

        public void setRequestHeader(string name, string value)
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation(name, value);
        }

        private void HandleResponse(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
        }

        public void DownloadFile(string uri, string fileStorePath)
        {
            var task = DownloadFileAsync(uri, fileStorePath);
            task.Wait();
        }

        public async Task DownloadFileAsync(string uri, string fileStorePath)
        {
            var stream = await client.GetStreamAsync(uri);
            var fileStream = new FileStream(fileStorePath, FileMode.Create, FileAccess.Write);
            using (stream)
            using (fileStream)
            {
                stream.CopyTo(fileStream);
            }
        }

        public string Get(string uri)
        {
            var task = GetAsync(uri);
            task.Wait();
            return task.Result;
        }

        public async Task<string> GetAsync(string uri)
        {
            var response = await client.GetAsync(uri);
            HandleResponse(response);
            var rawText = await response.Content.ReadAsStringAsync();
            return rawText;
        }

        public string PostJson(string uri, object data)
        {
            var jsonStr = JsonConvert.SerializeObject(data);
            return Post(uri, jsonStr, ContentTypes.Json);
        }

        public string Post(string uri, string data, string contentType)
        {
            var task = PostAsync(uri, data, contentType);
            task.Wait();
            return task.Result;
        }

        public async Task<string> PostAsync(string uri, string data, string contentType)
        {
            var content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            var response = await client.PostAsync(uri, content);
            HandleResponse(response);
            var rawText = await response.Content.ReadAsStringAsync();
            return rawText;
        }

        public void Dispose()
        {
            client?.Dispose();
            client = null;
        }
    }

    public class Headers
    {
        public static string UserAgent { get { return "User-Agent"; } }
    }


    public class UserAgents
    {
        public static string Chrome { get { return "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.87 Safari/537.36"; } }
    }

    public class ContentTypes
    {
        public static string Json { get { return "application/json"; } }

        public static string PlainText { get { return "text/plain"; } }
    }

}