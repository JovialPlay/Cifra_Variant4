using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using API.Model;
using API.Serivces;
using Cifra_Variant4_;
using System.Diagnostics;
using API.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class DataController : ControllerBase
    {
        public ConsoleApp Analyzer = new ConsoleApp();
        FileAnalyzerService fileAnalyzerService = new FileAnalyzerService();

        private readonly MainContext _context;

        public DataController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data>>> GetGames()
        {

            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Test_Data.txt");
            StreamWriter fileStream = new StreamWriter(filePath);

            List<Data> data = _context.Data.ToList();
            foreach (Data element in data)
            {
                fileStream.WriteLine(element.Key.ToString() + " = " + element.Value.ToString());
            }
            fileStream.Close();

            List<int>? answer = Analyzer.SendData(filePath);
            if (answer == null)
            {
                throw new ArgumentException("Ошибка! Отказано в доступе");
            }
            else
            {
                if (answer.Count == 1)
                {
                    return fileAnalyzerService.ReadDataFromFile(filePath);
                }
                else
                {
                    List<Data> newData = fileAnalyzerService.ReadDataFromFile(filePath);
                    int i = 0;
                    foreach (Data element in newData)
                    {
                        if (answer[i] != -1)
                        {
                            data[answer[i]].Value = newData[answer[i]].Value;
                            _context.Entry(data[answer[i]]).State = EntityState.Modified;
                            i++;
                        }
                    }
                    await _context.SaveChangesAsync();
                    return fileAnalyzerService.ReadDataFromFile(filePath);
                }
            }
        }
    }
}
