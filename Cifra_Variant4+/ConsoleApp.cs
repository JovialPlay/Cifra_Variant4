using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifra_Variant4_
{
    public class ConsoleApp
    {
        public List<int>? SendData(string filepath)
        {
            List<int> changedData = new List<int>();
            while (true)
            {
                string data = File.ReadAllText(filepath);
                Console.WriteLine(data);
                Console.WriteLine();
                Console.WriteLine("Выберите действие над файлом");
                Console.WriteLine("<Число> - Редактировать выбранные данные");
                Console.WriteLine("Отправить - отправить сообщение");
                Console.WriteLine("Закрыть - Закрыть программу");

                string? line = Console.ReadLine();
                switch (line)
                {
                    case "":
                    case "Закрыть": //
                        return null;
                    case "Отправить":
                        Console.Clear();
                        Console.WriteLine("Отправка сообщения");
                        //Thread.Sleep(30000);
                        Console.WriteLine("Сообщение отправлено");
                        changedData.Add(-1);
                        return changedData;
                    default:
                        try
                        {
                            int lineNumber;
                            if (!int.TryParse(line, out lineNumber))
                            {
                                throw new Exception("Ошибка! Введенные данные не являются числом");
                            }
                            lineNumber--;

                            if (lineNumber < 0)
                            {
                                throw new Exception("Ошибка! Номер строки меньше либо равен 0");
                            }
                            string? beforeChosenLine = "";
                            string? afterChosenLine = "";
                            string currentLine = "";

                            long position = 0;

                            FileStream file = File.OpenRead(filepath);
                            StreamReader reader = new StreamReader(file);
                            for (int i = 0; i < lineNumber; i++)
                            {
                                beforeChosenLine += reader.ReadLine() + "\n";
                                if (reader.EndOfStream)
                                {
                                    reader.Close();
                                    file.Close();
                                    throw new ArgumentException("Ошибка ввода данных! Данная строка отсутствует");
                                }
                            }

                            currentLine = reader.ReadLine();
                            int equalPos = 0;
                            while (currentLine[equalPos] != '=')
                            {
                                equalPos++;
                            }
                            currentLine = currentLine.Substring(0, equalPos + 2);

                            afterChosenLine = reader.ReadToEnd();
                            reader.Close();
                            file.Close();

                            Console.WriteLine("Введите новое значение данного аргумента");

                            string? newData = Console.ReadLine();

                            if (newData == "")
                            {
                                throw new ArgumentException("Ошибка ввода данных. Значение не может быть пустым");
                            }

                            file = File.Create(filepath);
                            StreamWriter writer = new StreamWriter(file);
                            file.Position = position;
                            writer.Write(beforeChosenLine);
                            writer.Write(currentLine + newData + "\n");
                            writer.Write(afterChosenLine);
                            writer.Close();
                            Console.Clear();
                            changedData.Add(lineNumber);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                            Console.WriteLine("Нажмите любую клавишу чтобы продолжить...");
                            Console.ReadKey();
                            Console.Clear();
                            return null;
                        }
                        break;
                }
            }
        }
    }
}
