using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace FiguresSerialize
{
    [Serializable]
    public class Figure
    {
        public string Name { get; set; }
        public int SideCount { get; set; }
        public double SideLenght { get; set; }
        public Figure Figure1 { get;  set; }

        public static string SerializeToJSON(Figure figure)
        {
            string json = JsonConvert.SerializeObject(figure);
            return json;
        }

        public static Figure DeserializeFromJSON(string json)
        {
            Figure figure = JsonConvert.DeserializeObject<Figure>(json);
            return figure;
        }
        
        public static string SerializeToXML(Figure figure)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Figure));
            string xml = null;
            return xml;
        }

        public void DeserializeFromXML(string xml)
        {
            
        }
        
        public byte[] SerializeToBinary()
        {
            byte[] binary = null;
            return binary;
        }

        public void DeserializeFronBinary(byte[] binary)
        {
            
        }
    }
}