using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleTranslator {
    class Program {
        static async Task Main (string[] args) {

            var url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=ru&dt=t&q=hello";
            using var client = new HttpClient ();

            var response = await client.GetAsync (url);

            string result = response.Content.ReadAsStringAsync ().Result;
            var output = JsonConvert.DeserializeObject<List<dynamic>> (result);
            Console.WriteLine (output[0][0][0]);
        }
    }
}