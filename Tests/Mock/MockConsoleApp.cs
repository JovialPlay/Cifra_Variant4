using Cifra_Variant4_;


namespace Tests.Mock
{
    public class MockConsoleApp
    {
        public ConsoleApp ConsoleApp = new ConsoleApp();
        public MockConsoleApp() 
        {
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Test_Data.txt");
            File.Create(filePath).Close();
            StreamWriter fileStream = new StreamWriter(filePath);

            fileStream.WriteLine("1 = 111");
            fileStream.WriteLine("2 = 222");
            fileStream.Close();

            ConsoleApp.filepath = filePath;
        }
    }
}
