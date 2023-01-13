using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDictionaryMvvmExample01.Models
{
    public  class Result
    {
        public Result(string type, string name, Dictionary<string, List<string>> partners)
        {
            Type = type;
            Name = name;
            Partners = partners;
        }

        public string Type { get; set; }
        public string Name { get; set; }
        public Dictionary<string, List<string>> Partners { get; set; }
    }
}

