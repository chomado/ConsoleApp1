﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherAsync().Wait();
            Console.ReadKey(); // 何かキーを押すまでプログラムが終了しない
        }

        private static async Task WeatherAsync()
        {
            var weatherApi = "http://weather.livedoor.com/forecast/webservice/json/v1?city=400040";

            // .NET で HTTP を扱うには、HttpClient というクラスを使う
            var client = new HttpClient();


            // Webからデータを「非同期で」取ってくる
            // (C# では、他の言語のような「コールバック」を使わず、await と書くことで、非同期処理の完了を待つことができる)
            var response = await client.GetAsync(weatherApi);

            // レスポンスから body のテキストを読み取る
            var json = await response.Content.ReadAsStringAsync();

            // 標準出力に表示。取得した生のJSONデータがコンソールに出力される
            //Console.WriteLine(json);

            // Json.NET で、JSON をデシリアライズします。
            // ここで、C#の機能のひとつである dynamic という、型の無い動的なオブジェクトとして受け取ります
            var result = JsonConvert.DeserializeObject<Rootobject>(json);
            Console.WriteLine(result.description.text);
        }
    }

    public class Rootobject
    {
        public Pinpointlocation[] pinpointLocations { get; set; }
        public string link { get; set; }
        public Forecast[] forecasts { get; set; }
        public Location location { get; set; }
        public DateTime publicTime { get; set; }
        public Copyright copyright { get; set; }
        public string title { get; set; }
        public Description description { get; set; }
    }

    public class Location
    {
        public string city { get; set; }
        public string area { get; set; }
        public string prefecture { get; set; }
    }

    public class Copyright
    {
        public Provider[] provider { get; set; }
        public string link { get; set; }
        public string title { get; set; }
        public Image image { get; set; }
    }

    public class Image
    {
        public int width { get; set; }
        public string link { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public int height { get; set; }
    }

    public class Provider
    {
        public string link { get; set; }
        public string name { get; set; }
    }

    public class Description
    {
        public string text { get; set; }
        public DateTime publicTime { get; set; }
    }

    public class Pinpointlocation
    {
        public string link { get; set; }
        public string name { get; set; }
    }

    public class Forecast
    {
        public string dateLabel { get; set; }
        public string telop { get; set; }
        public string date { get; set; }
        public Temperature temperature { get; set; }
        public Image1 image { get; set; }
    }

    public class Temperature
    {
        public Min min { get; set; }
        public Max max { get; set; }
    }

    public class Min
    {
        public string celsius { get; set; }
        public string fahrenheit { get; set; }
    }

    public class Max
    {
        public string celsius { get; set; }
        public string fahrenheit { get; set; }
    }

    public class Image1
    {
        public int width { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public int height { get; set; }
    }

}
