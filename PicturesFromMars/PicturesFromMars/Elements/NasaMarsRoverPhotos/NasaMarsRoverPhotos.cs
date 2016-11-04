using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Threading.Tasks;

using PicturesFromMars.Model;
using PicturesFromMars.WebAPI;
using Newtonsoft.Json;
using System.Net.Http;

namespace PicturesFromMars.NasaMarsRoverPhotos
{
    public static class NasaMarsRoverPhotos
    {
        private const string marsRoverPhotosUri = "https://api.nasa.gov/mars-photos/api/v1/rovers/";
        private const string marsRoverPhotosSolUri = "/photos?sol=";
        private const string marsRoverPhotosCameraUri = "&camera=";
        private const string marsRoverPhotosKeyUri = "&api_key=";

        public static async Task<int> GetRoverMaxSol(string rover, string key)
        {
            string testResponse = await SendTestRequest(1, rover, key);

            //deserialize response
            if (testResponse != null)
            {
                NasaRoversAPI g = new NasaRoversAPI();
                g = JsonConvert.DeserializeObject<NasaRoversAPI>(testResponse.ToString());

                //use response
                if (g != null)
                {
                    if (g.photos.Count() > 0)
                    {
                        return g.photos[0].rover.max_sol;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public static async Task<string> SendTestRequest(int sol, string rover, string key)
        {
            string completeUri = marsRoverPhotosUri + rover + marsRoverPhotosSolUri + sol.ToString() + marsRoverPhotosKeyUri + key;

            HttpClient client = WebAPI.WebAPI.CreateHttpClient(completeUri);

            Task.WaitAll();
            HttpResponseMessage response = await client.GetAsync(completeUri);


            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = string.Empty;

                string lineSeparator = ((char)0x2028).ToString();
                string paragraphSeparator = ((char)0x2029).ToString();

                jsonResponse = await response.Content.ReadAsStringAsync();
                jsonResponse = jsonResponse.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);

                return jsonResponse;
            }
            else
            {
                return string.Empty;
            }
        }

        public static async Task<string> SendPictureRequest(int sol, string camera, string rover, string key)
        {
            string completeUri = marsRoverPhotosUri + rover + marsRoverPhotosSolUri + sol.ToString() + marsRoverPhotosCameraUri + camera + marsRoverPhotosKeyUri + key;

            HttpClient client = PicturesFromMars.WebAPI.WebAPI.CreateHttpClient(completeUri);

            HttpResponseMessage response = await client.GetAsync(completeUri);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = string.Empty;

                string lineSeparator = ((char)0x2028).ToString();
                string paragraphSeparator = ((char)0x2029).ToString();

                jsonResponse = await response.Content.ReadAsStringAsync();
                jsonResponse = jsonResponse.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);

                return jsonResponse;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
