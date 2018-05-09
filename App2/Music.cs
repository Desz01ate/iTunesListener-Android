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

namespace iTunesListener
{
    class Music
    {
        public string Name { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public override bool Equals(object obj)
        {
            var obj2 = (Music)obj;
            return (obj2.Name == this.Name) && (obj2.Album == this.Album) && (obj2.Artist == this.Artist);
        }
    }
}