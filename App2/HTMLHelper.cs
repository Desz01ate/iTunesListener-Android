﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace iTunesListener
{
    class HTMLHelper
    {
        public static string Image_source
        {
            get
            {
                return "<img.+?src=[\"'](.+?)[\"'].+?>";
            }
        }

        public static async Task<List<string>> HTMLParser(string searchingKeyword, string regexPattern = "<img.+?src=[\"'](.+?)[\"'].+?>")
        {     
            var url = $@"https://www.google.co.th/search?q={searchingKeyword.Replace(" ","+")}&source=lnms&tbm=isch&sa=X&ved=0ahUKEwjl0-jRz8zZAhUIjJQKHRhpAwQQ_AUICigB&biw=1065&bih=557";
            var ResultList = new List<string>();
            try
            {
                HttpClient http = new HttpClient();
                var response = await http.GetByteArrayAsync(url);
                var source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                var result = Regex.Matches(source, regexPattern, RegexOptions.IgnoreCase);
                for (var i = 1; i < result.Count; i++) //skip index 0, it's checksum
                {
                    ResultList.Add(result[i].Groups[1].Value); //get src group from img
                }
            }
            catch
            {
                throw new Exception();
            }
            return ResultList;
        }
    }
}