using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASA_API_PHOTOSHOTS.Models
{
    public class ApodPicutrePresented
    {
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
    }
}
