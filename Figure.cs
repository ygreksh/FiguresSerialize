using System;
using System.Collections.Generic;
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

        public static void SerializeToJSON(FileStream fs, List<Figure> listOfFigures)
        {
            string json = JsonConvert.SerializeObject(listOfFigures);
            byte[] arrayjson = System.Text.Encoding.Default.GetBytes(json);
            fs.Write(arrayjson,0,arrayjson.Length);
        }

        public static List<Figure> DeserializeFromJSON(FileStream fs)
        {
            byte[] arrayjson = new byte[fs.Length];
            fs.Read(arrayjson,0,arrayjson.Length);
            string json = System.Text.Encoding.Default.GetString(arrayjson);
            List<Figure> listOfFigures = JsonConvert.DeserializeObject<List<Figure>>(json);
            return listOfFigures;
        }
        
        public static void SerializeToXML(FileStream fs, List<Figure> listOfFigures)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Figure>));
            formatter.Serialize(fs, listOfFigures);
        }

        public static List<Figure> DeserializeFromXML(FileStream fs)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Figure>));
            List<Figure> listOfFigures = (List<Figure>)formatter.Deserialize(fs);
            return listOfFigures;
        }
        
        public static void SerializeToBinary(FileStream fs, List<Figure> listOfFigures)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, listOfFigures);
        }

        public static List<Figure> DeserializeFronBinary(FileStream fs)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            List<Figure> listOfFigures = (List<Figure>)formatter.Deserialize(fs);
            return listOfFigures;
        }
    }
}