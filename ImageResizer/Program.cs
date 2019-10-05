using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    /// <summary>
    /// Program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);

            // 同步
            Stopwatch sw = new Stopwatch();
            sw.Start();
            imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            sw.Stop();
            var TaskSec = sw.ElapsedMilliseconds;

            Console.WriteLine($"[同步] 花費時間: {sw.ElapsedMilliseconds} ms");

            imageProcess.Clean(destinationPath);

            // 非同步
            sw.Restart();
            await imageProcess.ResizeImagesByAsync(sourcePath, destinationPath, 2.0);
            sw.Stop();
            var AsyncTaskSec = sw.ElapsedMilliseconds;

            Console.WriteLine($"[非同步] 花費時間: {AsyncTaskSec} ms");

            Console.WriteLine($"提升 : {((TaskSec - AsyncTaskSec) / (double)TaskSec * 100).ToString("#.#")} %");

            Console.ReadKey();
        }
    }
}
