using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace FiguresSerialize
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Figure> listOfFigures = new List<Figure>();
            Figure hexagon = new Figure() { Name = "Hexagon", SideCount = 6, SideLenght = 1 };
            Figure octagon = new Figure() { Name = "Octagon", SideCount = 8, SideLenght = 1 };
            hexagon.Figure1 = octagon;
            //вызовет ошибку из-за циклического вызова
            //octagon.Figure1 = octagon;    
            
            listOfFigures.Add(new Figure() { Name = "Triangle", SideCount = 3, SideLenght = 1 });
            listOfFigures.Add(new Figure() { Name = "Square", SideCount = 4, SideLenght = 1 });
            listOfFigures.Add(new Figure() { Name = "Pentagon", SideCount = 5, SideLenght = 1 });
            listOfFigures.Add(hexagon);
            listOfFigures.Add(octagon);
            
            string path = Path.Combine("Figures");
            DirectoryInfo MainDirectoryInfo = new DirectoryInfo(path);
            string FiguresfileNameJSON = "FiguresJSON.txt";
            string FiguresfileNameXML = "FiguresXML.xml";
            string FiguresfileNameBinary = "FiguresBinary.bin";
            if (!MainDirectoryInfo.Exists)
            {
                MainDirectoryInfo.Create();
            }
            //  JSON
            using (FileStream fileStream = new FileStream($"{path}\\{FiguresfileNameJSON}", FileMode.Create))
            {
                SerializeToJSON(fileStream, listOfFigures);
            }
            List<Figure> listFromJSON;
            using (FileStream fileStream = new FileStream($"{path}\\{FiguresfileNameJSON}", FileMode.Open))
            {
                listFromJSON = DeserializeFromJSON(fileStream);
            }
            Console.WriteLine("From JSON:");
            foreach (var figure in listFromJSON)
            {
                Console.WriteLine($"    {figure.Name} ({figure.SideCount},{figure.SideLenght})");
            }
            //  XML
            using (FileStream fileStream = new FileStream($"{path}\\{FiguresfileNameXML}", FileMode.Create))
            {
                SerializeToXML(fileStream, listOfFigures);
            }
            Console.WriteLine("From XML:");
            List<Figure> listFromXML;
            using (FileStream fileStream = new FileStream($"{path}\\{FiguresfileNameXML}", FileMode.Open))
            {
                listFromXML = DeserializeFromXML(fileStream);
            }
            foreach (var figure in listFromXML)
            {
                Console.WriteLine($"    {figure.Name} ({figure.SideCount},{figure.SideLenght})");
            }
            //  Binary
            using (FileStream fileStream = new FileStream($"{path}\\{FiguresfileNameBinary}", FileMode.Create))
            {
                SerializeToBinary(fileStream, listOfFigures);
            }
            Console.WriteLine("From BIN:");
            List<Figure> listFromBinary;
            using (FileStream fileStream = new FileStream($"{path}\\{FiguresfileNameBinary}", FileMode.Open))
            {
                listFromBinary = DeserializeFronBinary(fileStream);
            }
            foreach (var figure in listFromBinary)
            {
                Console.WriteLine($"    {figure.Name} ({figure.SideCount},{figure.SideLenght})");
            }
         }
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