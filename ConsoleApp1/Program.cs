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
            Console.WriteLine(json);
        }
    }
}
