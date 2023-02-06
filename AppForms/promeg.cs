using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppForms
{
    [Serializable]
    public class promeg
    {
        [JsonProperty("classname")]

        public string Class { get; set; }
        public int Z { get; set; }
        public int Y { get; set; }

        public int X { get; set; }

        public promeg()
        {
            
        }
        public promeg(int x, int y, int z,string classs)
        {
            Y = y;
            X = x;
            Z = z;
            Class = classs;
        }
        public promeg(int x, int y, string classs)
        {
            Y = y;
            X = x;
            Class = classs;
        }

        public override string ToString()
        {
            return string.Format($"({Class})");
        }
    }
}
