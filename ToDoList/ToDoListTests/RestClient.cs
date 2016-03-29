using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace Rest
{
    public struct Response
    {
        public HttpStatusCode Status { get; set; }

        public dynamic Data { get; set; }
    }

    public class RestClient
    {
        public RestClient(string domain)
        {
            this.domain = new Uri(domain);
        }

        private Uri domain;

        /// <summary>
        /// Creates an HttpClient for communicating with GitHub.  The GitHub API requires specific information
        /// to appear in each request header.
        /// </summary>
        private HttpClient CreateClient()
        {
            // Create a client whose base address is the GitHub server
            HttpClient client = new HttpClient();
            client.BaseAddress = domain;

            // There is more client configuration to do, depending on the request.
            return client;
        }

        public async Task<dynamic> DoGetAsync(string url, params string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = Uri.EscapeDataString(args[i]);
            }

            using (HttpClient client = CreateClient())
            {
                url = String.Format("ToDo.svc/" + url, args);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    return new Response { Status = response.StatusCode, Data = JsonConvert.DeserializeObject(result) };
                }
                else
                {
                    return new Response { Status = response.StatusCode };
                }
            }
        }
        public async Task<dynamic> DoPostAsync(dynamic data, string url)
        {
            using (HttpClient client = CreateClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("ToDo.svc/" + url, content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return new Response { Status = response.StatusCode, Data = JsonConvert.DeserializeObject(result) };
                }
                else
                {
                    return new Response { Status = response.StatusCode };
                }
            }
        }

        public async Task<dynamic> DoPutAsync(dynamic data, string url)
        {
            using (HttpClient client = CreateClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("ToDo.svc/" + url, content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return new Response { Status = response.StatusCode, Data = JsonConvert.DeserializeObject(result) };
                }
                else
                {
                    return new Response { Status = response.StatusCode };
                }
            }
        }

        public async Task<dynamic> DoDeleteAsync(string url)
        {
            using (HttpClient client = CreateClient())
            {
                HttpResponseMessage response = await client.DeleteAsync("ToDo.svc/" + url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return new Response { Status = response.StatusCode, Data = JsonConvert.DeserializeObject(result) };
                }
                else
                {
                    return new Response { Status = response.StatusCode };
                }
            }
        }
    }
}
