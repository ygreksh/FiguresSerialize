using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FiguresSerialize
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Figure> listOfFigures = new List<Figure>();
            Figure hexagon = new Figure() { Name = "Hexagon", SideCount = 6, SideLenght = 1 };
            Figure octagon = new Figure() { Name = "Oktagon", SideCount = 8, SideLenght = 1 };
            hexagon.Figure1 = octagon;
            //вызовет ошибку из-за циклического вызова
            octagon.Figure1 = octagon;    
            
            listOfFigures.Add(new Figure() { Name = "Triangle", SideCount = 3, SideLenght = 1 });
            listOfFigures.Add(new Figure() { Name = "Square", SideCount = 4, SideLenght = 1 });
            listOfFigures.Add(new Figure() { Name = "Pentagon", SideCount = 5, SideLenght = 1 });
            listOfFigures.Add(hexagon);
            listOfFigures.Add(octagon);
            
            string path = Path.Combine("Fugures");
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
                Figure.SerializeToJSON(fileStream, listOfFigures);
            }
            List<Figure> listFromJSON;
            using (FileStream fileStream = new FileStream($"{path}\\{FiguresfileNameJSON}", FileMode.Open))
            {
                listFromJSON = Figure.DeserializeFromJSON(fileStream);
            }
            Console.WriteLine("From JSON:");
            foreach (var figure in listFromJSON)
            {
                Console.WriteLine($"    {figure.Name} ({figure.SideCount},{figure.SideLenght})");
            }
            //  XML
            using (FileStream fileStream = new FileStream($"{path}\\{FiguresfileNameXML}", FileMode.Create))
            {
                Figure.SerializeToXML(fileStream, listOfFigures);
            }
            Console.WriteLine("From XML:");
            List<Figure> listFromXML;
            using (FileStream fileStream = new FileStream($"{path}\\{FiguresfileNameXML}", FileMode.Open))
            {
                listFromXML = Figure.DeserializeFromXML(fileStream);
            }
            foreach (var figure in listFromXML)
            {
                Console.WriteLine($"    {figure.Name} ({figure.SideCount},{figure.SideLenght})");
            }
            //  Binary
            using (FileStream fileStream = new FileStream($"{path}\\{FiguresfileNameBinary}", FileMode.Create))
            {
                Figure.SerializeToBinary(fileStream, listOfFigures);
            }
            Console.WriteLine("From BIN:");
            List<Figure> listFromBinary;
            using (FileStream fileStream = new FileStream($"{path}\\{FiguresfileNameBinary}", FileMode.Open))
            {
                listFromBinary = Figure.DeserializeFronBinary(fileStream);
            }
            foreach (var figure in listFromBinary)
            {
                Console.WriteLine($"    {figure.Name} ({figure.SideCount},{figure.SideLenght})");
            }
         }
    }

}