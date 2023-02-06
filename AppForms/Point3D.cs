using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PointLib;

namespace AppForms
{
    [Serializable]
    public class Point3D: Point
    {
        [JsonProperty("classname")]
        public string Class = "Point3d";
        [JsonProperty("Z")]
        public int Z { get; set; }

        public Point3D()
        {
            Z = rnd.Next(10);
        }

        public Point3D(int x, int y, int z) : base(x, y)
        {
            Z = z;
        }
        public override double Metric()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public override string ToString()
        {
            return string.Format($"({X} , {Y}, {Z})");
        }

    }
}
