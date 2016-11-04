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

using Android.Graphics;
using System.Net;

namespace PicturesFromMars.Utilities.Xamarin
{
    public static class Pictures
    {
        public static Bitmap GetBitmapFromUrl(string url)
        {
            Bitmap imgBitmap = null;

            using (var webClient = new WebClient())
            {
                var imgBytes = webClient.DownloadData(url);
                if (imgBytes != null && imgBytes.Length > 0)
                {
                    imgBitmap = BitmapFactory.DecodeByteArray(imgBytes, 0, imgBytes.Length);
                }
            }

            return imgBitmap;
        }
    }
}