using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;

namespace CqrsApp
{
    public class Program
    {
        private const int TaskCount = 4;
        private const int SleepTime = 50;
        private const string Path = "test.txt";
        private static ConcurrentBag<string> Messages = new ConcurrentBag<string>();
        private static ConcurrentBag<int> Failures = new ConcurrentBag<int>();

        public static async Task Main(string[] args)
        {
            
            var tasks = Enumerable.Range(0, TaskCount).Select(x => GenerateTask(x));
            
            var sw = new Stopwatch();
            sw.Start();
            await Task.WhenAll(tasks);

            sw.Stop();
            int writeFails = Failures.Count(x => x % 2 == 0);
            int readFails = Failures.Count(x => x % 2 == 1);
            //foreach (var item in Messages)
            //{
            //    Console.WriteLine(item);
            //}
            Console.WriteLine($"totalRuntime {sw.ElapsedMilliseconds};");
            Console.WriteLine($"write Fails: {writeFails}/{TaskCount / 2}");
            Console.WriteLine($"read fails: {readFails}/{TaskCount / 2}");
        }

        public static Task GenerateTask(int x)
        {
            if (x % 2 == 0)
            {
                return GenerateWriteTask(SleepTime, 10, x);
            }

            return GenerateReadTask(SleepTime, x);
        }

        public static async Task GenerateReadTask(int sleepTime, int x)
        {
            try
            {
                await using (var fs = WaitForFile(Path, FileMode.OpenOrCreate, x))
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    await Task.Delay(sleepTime);
                    var result = new byte[fs.Length];
                    var count = await fs.ReadAsync(result, 0, (int)fs.Length);
                    sw.Stop();
                    Messages.Add($"Read {count} bytes in {sw.ElapsedMilliseconds}ms");
                }
            }
            catch (IOException e)
            {
                Messages.Add("ReadFailed");
            }
        }

        public static async Task GenerateWriteTask(int sleepTime, int fileLength, int x)
        {
            try
            {
                var data = Enumerable.Range(0, fileLength).Select(x => (byte)x).ToArray();

                await using (var fs = WaitForFile(Path, FileMode.OpenOrCreate, x))
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    await Task.Delay(sleepTime);
                    await fs.WriteAsync(data);
                    sw.Stop();
                    Messages.Add($"Wrote {fileLength} bytes in {sw.ElapsedMilliseconds}ms");
                }
            }
            catch (IOException e)
            {
                Messages.Add("WriteFailed");
            }
        }

        public static FileStream WaitForFile(string fullPath, FileMode mode, int x)
        {
            for (int numTries = 0; numTries < 10; numTries++)
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(fullPath, mode);
                    return fs;
                }
                catch (IOException)
                {
                    if (fs != null)
                    {
                        fs.Dispose();
                    }
                    Failures.Add(x);
                    Thread.Sleep(50);
                }
            }

            return null;
        }
    }
}