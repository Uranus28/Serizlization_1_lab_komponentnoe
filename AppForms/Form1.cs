using System;
using PointLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Dynamic;
using System.Linq;

namespace AppForms
{
    public partial class Form1 : Form
        
    {
        public string serialized = "";
        private Point[] points = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            points = new Point[5];

            var rnd = new Random();

            for (int i = 0; i < points.Length; i++)
                points[i] = rnd.Next(3) % 2 == 0 ? new Point() : new Point3D();


            listBox.DataSource = points;

        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (points == null)
                return;

            Array.Sort(points);

            listBox.DataSource = null;
            listBox.DataSource = points;

        }

        private void btnSerialize_Click(object sender, EventArgs e)
        {

            var dlg = new SaveFileDialog();
            dlg.Filter = "SOAP|*.soap|XML|*.xml|JSON|*.json|Binary|*.bin";

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            using (var fs =
                new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
            {
                switch (Path.GetExtension(dlg.FileName))
                {
                    case ".bin":
                        var bf = new BinaryFormatter();
                        bf.Serialize(fs, points);
                        break;
                    case ".soap":
                        var sf = new SoapFormatter();
                        sf.Serialize(fs, points);
                        break;
                    case ".xml":
                        var xf = new XmlSerializer(typeof(Point[]), new[] { typeof(Point3D) });
                        xf.Serialize(fs, points);
                        break;
                    case ".json":
                        var jf = new JsonSerializer();
                        jf.NullValueHandling = NullValueHandling.Ignore;

                        using (StreamWriter sw = new StreamWriter(fs))
                        using (JsonWriter writer = new JsonTextWriter(sw))
                        {
                            jf.Serialize(writer, points);
                        }
                        
                        break;

                }
            }

        }

        private void btnDeserialize_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "SOAP|*.soap|XML|*.xml|JSON|*.json|Binary|*.bin";

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            using (var fs =
                new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
            {
                switch (Path.GetExtension(dlg.FileName))
                {
                    case ".bin":
                        var bf = new BinaryFormatter();
                        points = (Point[])bf.Deserialize(fs);
                        break;
                    case ".soap":
                        var sf = new SoapFormatter();
                        points = (Point[])sf.Deserialize(fs);
                        break;
                    case ".xml":
                        var xf = new XmlSerializer(typeof(Point[]), new[] { typeof(Point3D) });
                        points = (Point[])xf.Deserialize(fs);
                        break;
                    case ".json":

                        var jf = new JsonSerializer();
                        promeg[] pr = null;
                        using (var r = new StreamReader(fs))
                            pr = (promeg[])jf.Deserialize(r, typeof(promeg[]));

                        points = null;
                        points = new Point[5];

                        int k = 0;
                        foreach(promeg i in pr)
                        {
                            if (i.ToString() == "(Point)")
                            {
                                points[k] = new Point(i.X, i.Y);
                                k++;
                            }
                            else
                            {
                                points[k] = new Point3D(i.X, i.Y,i.Z);
                                k++;

                            }
                        }
                       
                        break;

                }
            }
            listBox.DataSource = null;
            listBox.DataSource = points;
        }
    }
}
