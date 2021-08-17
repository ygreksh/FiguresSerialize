using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

        public static void SerializeToJSON(FileStream fs, Figure figure)
        {
            string json = JsonConvert.SerializeObject(figure);
            byte[] arrayjson = System.Text.Encoding.Default.GetBytes(json);
            fs.Write(arrayjson,0,arrayjson.Length);
        }

        public static Figure DeserializeFromJSON(FileStream fs)
        {
            byte[] arrayjson = new byte[fs.Length];
            fs.Read(arrayjson,0,arrayjson.Length);
            string json = System.Text.Encoding.Default.GetString(arrayjson);
            Figure figure = JsonConvert.DeserializeObject<Figure>(json);
            return figure;
        }
        
        public static void SerializeToXML(FileStream fs, Figure figure)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Figure));
            formatter.Serialize(fs, figure);
        }

        public Figure DeserializeFromXML(FileStream fs)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Figure));
            Figure figure = (Figure)formatter.Deserialize(fs);
            return figure;
        }
        
        public static void SerializeToBinary(FileStream fs, Figure figure)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, figure);
        }

        public static Figure DeserializeFronBinary(FileStream fs)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Figure figure = (Figure)formatter.Deserialize(fs);
            return figure;
        }
    }
}