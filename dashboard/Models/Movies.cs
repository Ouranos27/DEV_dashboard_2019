using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dashboard.Models
{
    public class Result
    {
        public string   Tittle { get; set; }
        public string   OriginalLanguage { get; set; }
        public string   Overview { get; set; }
        public string   ReleaseDate { get; set; }
    }

    public class Movies
    {
        public List<Result>    Results { get; set; }
    }
}
