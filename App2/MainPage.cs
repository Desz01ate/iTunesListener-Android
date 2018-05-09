using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace iTunesListener
{
    [Activity(Label = "MainPage")]
    public class MainPage : Fragment
    {
        private static HttpClient client;
        private static bool isPlay;
        private TextView name, album, artist;
        private ImageView image;
        private ManualResetEvent _event = new ManualResetEvent(true);
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.content_main, container, false);
            var prev = view.FindViewById<Button>(Resource.Id.content_main_prev);
            var playPause = view.FindViewById<Button>(Resource.Id.content_main_play_pause);
            var next = view.FindViewById<Button>(Resource.Id.content_main_next);
            name = view.FindViewById<TextView>(Resource.Id.content_main_name);
            album = view.FindViewById<TextView>(Resource.Id.content_main_album);
            artist = view.FindViewById<TextView>(Resource.Id.content_main_artist);
            image = view.FindViewById<ImageView>(Resource.Id.content_main_image);
            client = new HttpClient();
            prev.Click += PreviousAction;
            playPause.Click += PlayPauseAction;
            next.Click += NextAction;
            Activity.RunOnUiThread(TrackMonitor);
            return view;

        }
        public override void OnPause()
        {
            base.OnPause();
        }
        private async void TrackMonitor()
        {
            Music previousTrack = new Music();
            while (true)
            {
                _event.WaitOne();
                var result = await client.GetAsync("http://10.0.2.2/itunesSyncer/api/Status");
                var message = await result.Content.ReadAsStringAsync();
                var track = JsonConvert.DeserializeObject<Music>(message);
                if(!track.Equals(previousTrack))
                {
                    var HTMLresult = await HTMLHelper.HTMLParser($"{track.Name} {track.Album} {track.Artist} album artwork", HTMLHelper.Image_source);
                    image.SetImageBitmap(await ImageHelper.GetImageAsync(HTMLresult[0]));
                    name.Text = track.Name;
                    album.Text = track.Album;
                    artist.Text = track.Artist;
                    previousTrack = track;
                }
                await Task.Delay(1000);
            }
        }

        private void NextAction(object sender, EventArgs e)
        {
            var dict = new Dictionary<string, string>
            {
                { "stat", "Next" }
            };
            var content = new FormUrlEncodedContent(dict);
            client.PostAsync("http://10.0.2.2/iTunesSyncer/StatUpdate", content);
        }

        private void PlayPauseAction(object sender, EventArgs e)
        {
            var dict = new Dictionary<string, string>
            {
                { "stat", isPlay ? "Pause" : "Play" }
            };
            var content = new FormUrlEncodedContent(dict);
            client.PostAsync("http://10.0.2.2/iTunesSyncer/StatUpdate", content);
            isPlay = !isPlay;
        }

        private void PreviousAction(object sender, EventArgs e)
        {
            var dict = new Dictionary<string, string>
            {
                { "stat", "Previous" }
            };
            var content = new FormUrlEncodedContent(dict);
            client.PostAsync("http://10.0.2.2/iTunesSyncer/StatUpdate", content);
        }
    }
}