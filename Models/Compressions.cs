using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Compressions
    {
        public string originalName { get; set; }
        public string compressedName { get; set; }
        public string path { get; set; }
        public string method { get; set; }
        public double ratio { get; set; }
        public double factor { get; set; }
        public string percentage { get; set; }
    }
}
