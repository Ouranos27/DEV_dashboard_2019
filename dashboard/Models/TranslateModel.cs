using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dashboard.Models
{
    public class Translation
    {
        public string translatedText { get; set; }
    }

    public class Data
    {
        public List<Translation> translations { get; set; }
    }

    public class TranslateModel
    {
        public Data data { get; set; }
    }
}
