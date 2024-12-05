string filepath = "C:\\Other\\Study\\Programming\\C#\\Cifra_Variant4+\\Cifra_Variant4+\\information.txt";

//Если файл не найден по изначальному пути
if (!File.Exists(filepath))
{
    string line;
    bool fileFound = false;
    while (!fileFound)
    {
        Console.WriteLine("Файл не найден!");
        Console.WriteLine("Новый - Создать новый файл");
        Console.WriteLine("Найти - Указать путь к файлу");
        Console.WriteLine("Закрыть - Завершить работу");

        string? newFilepath;

        line = Console.ReadLine();
        switch (line)
        {
            case "Новый": // Если введено 1
                Console.Clear();
                Console.WriteLine("Введите путь к файлу");
                newFilepath = Console.ReadLine();
                try
                {
                    FileStream newFile = File.Create(newFilepath);
                    newFile.Close();
                    filepath = newFilepath;
                    fileFound = true;
                }
                catch
                {
                    Console.WriteLine("Ошибка. Неправильно указан путь к файлу");
                    Console.WriteLine("Нажмите любую клавишу чтобы продолжить...");
                    Console.ReadKey();
                }
                break;
            case "Найти": //Если введено  2
                Console.Clear();
                Console.WriteLine("Введите путь к файлу");
                newFilepath = Console.ReadLine();
                if (newFilepath != null && File.Exists(newFilepath))
                {
                    filepath = newFilepath;
                    fileFound = true;
                }
                break;
            case "Закрыть": //Если введено  3
                return;
            default:
                Console.Clear();
                break;
        }
        Console.Clear();
    }
}

bool EndOfSession = false;
while (!EndOfSession)
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
        case "": case "Закрыть": //
            return;
        case "Отправить":
            Console.Clear();
            Console.WriteLine("Отправка сообщения");
            Thread.Sleep(30000);
            Console.WriteLine("Сообщение отправлено");
            break;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                Console.WriteLine("Нажмите любую клавишу чтобы продолжить...");
                Console.ReadKey();
                Console.Clear();
            }
            break;
    }
}
