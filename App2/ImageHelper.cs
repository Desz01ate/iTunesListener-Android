using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace iTunesListener
{
    class ImageHelper
    {
        public static async System.Threading.Tasks.Task<Bitmap> GetImageAsync(string url)
        {
            WebClient client = new WebClient();
            var byteArray = client.DownloadData(url);
            return await BitmapFactory.DecodeByteArrayAsync(byteArray, 0, byteArray.Length);
        }
    }
}