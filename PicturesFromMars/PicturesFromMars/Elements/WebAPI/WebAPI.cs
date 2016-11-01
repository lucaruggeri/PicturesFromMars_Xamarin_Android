using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;

namespace PicturesFromMars.WebAPI
{
    public static class WebAPI
    {
        public static HttpClient CreateHttpClient(string uri)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);

            return client;
        }

    }
}
