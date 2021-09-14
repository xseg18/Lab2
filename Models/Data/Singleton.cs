using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models.Data
{
    public class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public List<Compressions> Compressions { get; set; }

        private Singleton()
        {
            Compressions = new List<Compressions>();
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
