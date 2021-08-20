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
        protected string closedProperty { get; set; }

        public Figure()
        {
            Name = "empty";
            SideCount = 0;
            SideLenght = 0;
            closedProperty = "closedEmpty";
        }
        public Figure(string name, int sidecount, int sidelenght)
        {
            Name = name;
            SideCount = sidecount;
            SideLenght = sidelenght;
            closedProperty = "closedEmpty";
        }

        public void Display()
        {
            Console.WriteLine($"{Name}, сторон = {SideCount}, длина стороны = {SideLenght}, closedProperty = {closedProperty}");
        }

        public void Perimeter()
        {
            double perimeter = SideCount * SideLenght;
            Console.WriteLine($"Perimeter of {Name} = {perimeter}");
        }

        public void SetSideLenght(double sideLenght)
        {
            SideLenght = sideLenght;
        }
    }
}