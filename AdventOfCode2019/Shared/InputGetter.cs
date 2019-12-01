using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Shared
{
    public class InputGetter
    {
        public static string GetInputForDay(int day)
        {
            string cacheFileName = $"day{day}.input";
            if (File.Exists(cacheFileName))
            {
                return File.ReadAllText(cacheFileName);
            }
            var request = WebRequest.CreateHttp($"https://adventofcode.com/2019/day/{day}/input");
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("session", "53616c7465645f5f385c9eba8a10cfb2f473c3d7110a7b8642da5d30080c1eda8fb66115ed06097e14d5424612c45ae0", "/", "adventofcode.com"));
            var response = request.GetResponse();
            using var responseStream = response.GetResponseStream();
            using var reader = new StreamReader(responseStream);
            var input = reader.ReadToEnd();
            File.WriteAllText(cacheFileName, input);

            return input;
        }

        public static string[] GetSplitInputForDay(int day, char[] split)
        {
            var input = GetInputForDay(day);
            return input.Split(split, StringSplitOptions.RemoveEmptyEntries);
        }

        public static ICollection<T> GetTransformedSplitInputForDay<T>(int day, char[] split, Func<string, T> transformation)
        {
            return GetSplitInputForDay(day, split).Select(transformation).ToList();
        }
    }
}
