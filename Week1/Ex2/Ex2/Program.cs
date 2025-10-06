using System.Diagnostics;
using System.Threading.Tasks;

namespace Ex2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var files = new List<string>
            {
                "Файл1",
                "Файл2",
                "Файл3"
            };
            var syncStopwatch = Stopwatch.StartNew();
            foreach (var file in files)
            {
                var result = ProcessData(file);
                Console.WriteLine($"Обработка {result} завершена за 3 секунды");
            }
            syncStopwatch.Stop();
            Console.WriteLine($"Синхронная обработка завершена за {syncStopwatch.Elapsed.TotalSeconds:F2} секунд");

            var asyncStopwatch = Stopwatch.StartNew();
            var asyncFiles = new List<Task<string>>
            {
                ProcessDataAsync("Файл1"),
                ProcessDataAsync("Файл2"),
                ProcessDataAsync("Файл3")
            };
            while (asyncFiles.Count > 0)
            {
                var completedTask = await Task.WhenAny(asyncFiles);
                string result = await completedTask;
                Console.WriteLine($"Обработка {result} завершена за 3 секунды");
                asyncFiles.Remove(completedTask); 
            }
            asyncStopwatch.Stop();
            Console.WriteLine($"Асинхронная обработка завершена за {asyncStopwatch.Elapsed.TotalSeconds:F2} секунд");
        }
        static string ProcessData(string dataName)
        {
            Thread.Sleep(3000);
            return dataName;
        }
        static async Task<string> ProcessDataAsync(string dataName)
        {
            await Task.Delay(3000);
            return dataName;
        }

    }
}
