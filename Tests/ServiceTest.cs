using API.Serivces;
using API.Model;
using Cifra_Variant4_;

namespace Tests
{
    public class ServiceTest
    {
        [Fact]
        public void Test()
        {
            FileAnalyzerService service = new FileAnalyzerService();

            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Test_Data.txt");
            File.Create(filePath).Close();
            StreamWriter fileStream = new StreamWriter(filePath);

            fileStream.WriteLine("1 = 111");
            fileStream.WriteLine("2 = 222");
            fileStream.Close();

            List<Data> data = service.ReadDataFromFile(filePath);
            Assert.Equal(2, data.Count);
            Assert.Equal("1", data[0].Key);
            Assert.Equal("111", data[0].Value);
            Assert.Equal("2", data[1].Key);
            Assert.Equal("222", data[1].Value);
        }
    }
}