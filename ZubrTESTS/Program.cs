using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ZubrTESTS
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var set1 = new List<string>()
            {
                "1",
                "0",
                "5"
            };
            var cartesianProduct=
                set1.SelectMany(p=> set1.Select(q=> new Tuple<string,string>(p,q)));
            Console.WriteLine(cartesianProduct);
        }
    }
}