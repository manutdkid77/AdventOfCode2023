using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adventofcode_2023.Services
{
    public class FileReader
    {
         public string[] ReadAllLines(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            return lines;
        }
    }
}