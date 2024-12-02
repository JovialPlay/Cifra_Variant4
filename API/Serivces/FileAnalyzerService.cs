using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Model;
using System.Reflection.Metadata.Ecma335;

namespace API.Serivces
{
    public class FileAnalyzerService
    {
        public List<Data> ReadDataFromFile(string filePath)
        {
            List<Data> data = new List<Data>();

            FileStream file = File.OpenRead(filePath);
            StreamReader reader = new StreamReader(file);

            while (!reader.EndOfStream)
            {
                string currentLine = reader.ReadLine();

                int equalPos = 0;
                while (currentLine[equalPos] != '=')
                {
                    equalPos++;
                }

                string Key = currentLine.Substring(0,equalPos - 1);
                string Value = currentLine.Substring(equalPos + 2);

                Data newData = new Data{ 
                    Key = Key, 
                    Value = Value };

                data.Add(newData);
            }

            reader.Close();
            file.Close();

            return data;
        }       
    }
}
