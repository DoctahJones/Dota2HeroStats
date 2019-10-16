using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;


namespace Dota2HeroStats.Services.GameFetcher
{
    public class APIRequestSenderJSON : IAPIRequestSender
    {
        public string MakeRequest(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("A Url has not been set.");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    Console.WriteLine("error in makeRequest: " + errorText);
                }
                throw;
            }
        }

        public async Task<string> MakeRequestAsync(string url, CancellationToken cancelToken, TimeSpan timeOut)
        {
            if (url == null)
            {
                throw new ArgumentNullException("A Url has not been set.");
            }
            try
            {
                HttpClient client = new HttpClient { Timeout = timeOut };
                HttpRequestMessage request = new HttpRequestMessage();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var result = await client.GetAsync(new Uri(url), HttpCompletionOption.ResponseContentRead, cancelToken);

                string jsonstring = await result.Content.ReadAsStringAsync();
                return jsonstring;
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    Console.WriteLine("error in makeRequestAsync: " + errorText);
                }
                throw;
            }
        }

    }
}
