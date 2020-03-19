using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleTranslator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string i = "y";
            while (i == "y")
            {
                var ats = await translator();
                Console.WriteLine(ats);

                Console.WriteLine("Versti dar karta? Jei taip - iveskite y");
                var option = Console.ReadLine();
                if (option != "y")
                {
                    i = option;
                }
            }

            static async Task<string> translator()
            {
                var text = await translatorMeniu();
                return text;
            }
        }
        static async Task<string> translatorMeniu()
        {
            Console.Clear();
            string word, langFrom, langTo1, langTo2;
            Console.WriteLine("(1) - Versti is lietuviu kalbos i anglu ir rusu kalbas");
            Console.WriteLine("(2) - Versti is anglu kalbos i rusu ir lietuviu kalbas");
            Console.WriteLine("(3) - Versti is rusu kalbos i anglu ir lietuviu kalbas");
            var option = Console.ReadLine();
            if (option == "1")
            {
                word = typeWord();
                langFrom = "lt";
                langTo1 = "en";
                langTo2 = "ru";
                return await translateText(word, langFrom, langTo1, langTo2);
            }
            else if (option == "2")
            {
                word = typeWord();
                langFrom = "en";
                langTo1 = "ru";
                langTo2 = "lt";
                return await translateText(word, langFrom, langTo1, langTo2);
            }
            else if (option == "3")
            {
                word = typeWord();
                langFrom = "ru";
                langTo1 = "en";
                langTo2 = "lt";
                return await translateText(word, langFrom, langTo1, langTo2);
            }
            return "Blogas pasirinkimas";
        }

        static string typeWord()
        {
            Console.WriteLine("Iveskite zodi:");
            string word = Console.ReadLine();
            return word;
        }

        static async Task<string> translateText(string word, string langFrom, string langTo1, string langTo2)
        {
            string url1 = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=" + langFrom + "&tl=" + langTo1 + "&dt=t&q=" + word;
            string url2 = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=" + langFrom + "&tl=" + langTo2 + "&dt=t&q=" + word;
            using var client = new HttpClient();

            var response1 = await client.GetAsync(url1);
            var response2 = await client.GetAsync(url2);

            string result1 = response1.Content.ReadAsStringAsync().Result;
            var output1 = JsonConvert.DeserializeObject<List<dynamic>>(result1);
            string result2 = response2.Content.ReadAsStringAsync().Result;
            var output2 = JsonConvert.DeserializeObject<List<dynamic>>(result2);
            StringBuilder ats = new StringBuilder();
            ats.AppendFormat("Vertimas:| {0} |  {1}  |", output1[0][0][0], output2[0][0][0]);
            ats.AppendLine();
            return ats.ToString();
        }
    }
}